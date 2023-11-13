using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using TechnoService.Model;

namespace TechnoService.Data
{
    public class TechnoServiceRepository
    {
        private TechnoServiceEntities technoServiceEntities;

        public TechnoServiceRepository(TechnoServiceEntities technoServiceEntities)
        {
            this.technoServiceEntities = technoServiceEntities;
        }

        public int CheckUserExists(string login, string password)
        {
            List<User> userList = technoServiceEntities.Users.ToList();
            foreach (User user in userList)
            {
                if (user.Login == login && user.Password == password)
                {
                    return user.Id;
                }
                else if (user.Login == login && user.Password != password)
                {
                    throw new Exception("Неверный пароль!");
                }
            }
            throw new Exception("Аккаунта с таким логином не существует!");
        }

        public User GetUserById(int userId)
        {
            User user = technoServiceEntities.Users.FirstOrDefault(_user => _user.Id == userId);
            if (user != null)
            {
                return user;
            }
            throw new Exception($"Пользователь с идентификатором {userId} не найден!");
        }

        public List<User> GetAllContractors()
        {
            List<User> contractors = technoServiceEntities.Users.Where(_user => _user.Role.Name == "Исполнитель").ToList();
            return contractors;
        }

        public List<Request> GetAllRequests()
        {
            List<Request> requests = technoServiceEntities.Requests.ToList();
            return requests;
        }

        public List<Request> GetRequestsByClientId(int clientId)
        {
            List<Request> clientRequests = technoServiceEntities.Requests.Where(_request => _request.ClientId == clientId).ToList();
            return clientRequests;
        }

        public List<Request> GetRequestsByContractorId(int contractorId)
        {
            List<Request> contractorRequests = technoServiceEntities.Requests.Where(_request => _request.ContractorId == contractorId).ToList();
            return contractorRequests;
        }

        public Request GetRequestById(int requestId)
        {
            Request request = technoServiceEntities.Requests.FirstOrDefault(_request => _request.Id == requestId);
            if (request != null)
            {
                return request;
            }
            throw new Exception($"Заявка с идентификатором {requestId} не найдена!");
        }

        public List<Request> GetRequestsByClientSearchQuery(string searchQuery, int clientId)
        {
            if (searchQuery.Length == 0)
            {
                return GetRequestsByClientId(clientId);
            }

            List<Request> searchingRequests = technoServiceEntities.Requests.Where(_request =>
                _request.ClientId == clientId &&
                (_request.Id.ToString().Contains(searchQuery) ||
                _request.Equipment.Name.Contains(searchQuery) ||
                _request.FaultType.Name.Contains(searchQuery) ||
                _request.Status.Name.Contains(searchQuery) ||
                _request.PublicationDate.ToString().Contains(searchQuery))
            ).ToList();
            return searchingRequests;
        }

        public List<Request> GetRequestsByManagerSearchQuery(string searchQuery)
        {
            if (searchQuery.Length == 0)
            {
                return GetAllRequests();
            }

            List<Request> searchingRequests = technoServiceEntities.Requests.Where(_request =>
                _request.Id.ToString().Contains(searchQuery) ||
                _request.Equipment.Name.Contains(searchQuery) ||
                _request.FaultType.Name.Contains(searchQuery) ||
                _request.Status.Name.Contains(searchQuery) ||
                _request.PublicationDate.ToString().Contains(searchQuery)
            ).ToList();
            return searchingRequests;
        }

        public List<Request> GetRequestsByContractorSearchQuery(string searchQuery, int contractorId)
        {
            if (searchQuery.Length == 0)
            {
                return GetRequestsByContractorId(contractorId);
            }

            List<Request> searchingRequests = technoServiceEntities.Requests.Where(_request =>
                _request.ContractorId == contractorId &&
                (_request.Id.ToString().Contains(searchQuery) ||
                _request.Equipment.Name.Contains(searchQuery) ||
                _request.FaultType.Name.Contains(searchQuery) ||
                _request.Status.Name.Contains(searchQuery) ||
                _request.PublicationDate.ToString().Contains(searchQuery))
            ).ToList();
            return searchingRequests;
        }

        public void AddRequest(int clientId, int equipmentId, int faultTypeId, string description)
        {
            Request request = new Request
            {
                EquipmentId = equipmentId,
                FaultTypeId = faultTypeId,
                ClientId = clientId,
                ContractorId = null,
                StatusId = 1,
                Description = description,
                Comment = string.Empty,
                PublicationDate = DateTime.Now,
                EndDate = null
            };
            technoServiceEntities.Requests.Add(request);
            technoServiceEntities.SaveChanges();
        }

        public void EditRequest(int requestId, int equipmentId, int faultTypeId, int statusId, int contractorId, string description, string comment, DateTime? endDate)
        {
            Request request = technoServiceEntities.Requests.FirstOrDefault(_request => _request.Id == requestId);
            request.EquipmentId = equipmentId;
            request.FaultTypeId = faultTypeId;
            request.StatusId = statusId;
            if (contractorId == -1)
            {
                request.ContractorId = null;
            }
            else
            {
                request.ContractorId = contractorId;
            }
            request.Description = description;
            request.Comment = comment;
            if (GetStatusById(statusId).Name == "Выполнено")
            {
                request.EndDate = DateTime.Now;
            }
            else
            {
                request.EndDate = null;
            }
            request.EndDate = endDate;
            technoServiceEntities.SaveChanges();
        }

        public List<Equipment> GetAllEquipment()
        {
            List<Equipment> equipment = technoServiceEntities.Equipments.ToList();
            return equipment;
        }

        public List<FaultType> GetAllFaultTypes()
        {
            List<FaultType> faultTypes = technoServiceEntities.FaultTypes.ToList();
            return faultTypes;
        }

        public List<Status> GetAllStatuses()
        {
            List<Status> statuses = technoServiceEntities.Status.ToList();
            return statuses;
        }

        public Status GetStatusById(int statusId)
        {
            Status status = technoServiceEntities.Status.FirstOrDefault(_status => _status.Id == statusId);
            if (status != null)
            {
                return status;
            }
            throw new Exception($"Статус с идентификатором {statusId} не найден!");
        }

        public int GetNumOfReadyRequests()
        {
            int NumOfReadyRequests = technoServiceEntities.Requests.Count(_request => _request.Status.Name == "Выполнено");
            return NumOfReadyRequests;
        }

        public double GetAvgCompletionTime()
        {
            double? avgCompletionTime = technoServiceEntities.Requests.Average(_request =>
            SqlFunctions.DateDiff("HOUR", _request.PublicationDate, _request.EndDate)
            );
            if (avgCompletionTime != null)
            {
                return (double)avgCompletionTime;
            }
            else
            {
                return -1.0;
            }
        }

        public List<FaultTypeStatistics> GetFaultTypeStatistics()
        {
            List<FaultTypeStatistics> faultTypeStatisticsList = new List<FaultTypeStatistics>();

            var pairs = technoServiceEntities.Requests.GroupBy(_request => _request.FaultType);
            foreach(var pair in pairs)
            {
                FaultTypeStatistics faultTypeStatistics = new FaultTypeStatistics {
                    FaultType = pair.Key,
                    Quantity = pair.Count()
                };
                faultTypeStatisticsList.Add(faultTypeStatistics);
            }

            return faultTypeStatisticsList;
        }
    }
}
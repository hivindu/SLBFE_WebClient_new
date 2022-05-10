using Newtonsoft.Json;
using SLBFE_WebClient.Models;
using SLBFE_WebClient.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SLBFE_WebClient.Services
{
    public class SLBFE_Services : ISLBFE_Services
    {
        private static HttpClient client;

        public async Task<bool> ApproveVacancy(string vacancyid)
        {
            var vacancyApproveRequest = new ApproveVacancyRequest() {
                VacancyId = vacancyid,
            };
            var company = JsonConvert.SerializeObject(vacancyApproveRequest);
            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:8001/api/Vacancies/ApproveVacancy.js", requestContent);

            if (response.IsSuccessStatusCode)
            { return true; }

            return false;
        }

        public async Task<bool> ChangeCitizenActivationStatus(string citizenId, bool status)
        {
            var request = new CitizenStatusChangeRequest() {
            IsActive = status,
            };
            var company = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:8001/api/Citizens/ChangeActivationStatus/"+ citizenId +".js", requestContent);

            if (response.IsSuccessStatusCode)
            { return true; }

            return false;
        }

        public async Task<bool> ChangeCompanyActivationStatus(string companyId, bool status)
        {
            var request = new StatusChangeRequest()
            {
                Id = companyId,
                Status = status,
            };
            var company = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:8001/api/Users/ChangeActivationStatus.js", requestContent);

            if (response.IsSuccessStatusCode)
            { return true; }

            return false;
        }

        public Users CreateCompanyAccount(CreateCompanyUserRequest request)
        {
            client = new HttpClient();
            var res = new Users();
            client.BaseAddress = new Uri("http://localhost:8001");
            var response = client.PostAsJsonAsync("api/Users/CompanyAccount.js", request).Result;
            if (response.IsSuccessStatusCode)
            {
                var vacancy = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<Users>(vacancy);
            }
            else
            {
                res = default;
            }

            return res;
        }

        public Users CreateUserAccount(CreateUserRequest request)
        {
            client = new HttpClient();
            var res = new Users();
            client.BaseAddress = new Uri("http://localhost:8001");
            var response = client.PostAsJsonAsync("api/Users/.js", request).Result;
            if (response.IsSuccessStatusCode)
            {
                var vacancy = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<Users>(vacancy);
            }
            else
            {
                res = default;
            }

            return res;
        }

        public JobVacencies CreateVacancy(CreateVacancyRequest vacancyRequest)
        {
            client = new HttpClient();
            var res = new JobVacencies();
            client.BaseAddress = new Uri("http://localhost:8001");
            var response = client.PostAsJsonAsync("api/Vacancies/.js", vacancyRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var vacancy = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<JobVacencies>(vacancy);
            }
            else
            {
                res = default;
            }

            return res;
        }

        public bool DeleteUser(string id)
        {
            client = new HttpClient();
            var res = false;
            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync("api/Users/" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                res = true;
            }
            else
            {
                res = default;
            }

            return res;
        }

        public bool DeleteVacancy(string id)
        {
            client = new HttpClient();
            var res = false;
            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync("api/Vacancies/"+id+"").Result;
            if (response.IsSuccessStatusCode)
            {
                res = true;
            }
            else
            {
                res = default;
            }

            return res;
        }

        public List<Citizen> GetAllCitizens()
        {
            client = new HttpClient();
            var res = new List<Citizen>();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Citizens/.js").Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<List<Citizen>>(user);
            }
            else
            {
                res = default;
            }
            return res;
        }

        public List<Users> GetAllCompanies()
        {
            client = new HttpClient();
            var res = new List<Users>();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Users/.js").Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsStringAsync().Result;
                var allUsers = JsonConvert.DeserializeObject<List<Users>>(user);

                if (allUsers != null)
                {
                    foreach (var singleUser in allUsers)
                    {
                        if (singleUser.UserType == 2)
                        {
                            res.Add(singleUser);
                        }
                    }
                }
            }
            else
            {
                res = default;
            }
            return res;
        }

        public List<ComplaintViewResponse> GetAllComplaints()
        {
            client = new HttpClient();
            var res = new List<Complaint>();
            var citizensResponse = new List<ComplaintViewResponse>();
            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Complaint/.js").Result;
            if (response.IsSuccessStatusCode)
            {
                var complaints = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<List<Complaint>>(complaints);

                if (res != null && res.Any())
                {
                    foreach (var complaint in res)
                    {
                        if(string.IsNullOrEmpty(complaint.ResponseMessage))
                        {
                            var citiizen = GetCitizenById(complaint.UserId);
                            if (citiizen != null)
                            {
                                citizensResponse.Add(new ComplaintViewResponse()
                                {
                                    ComplainMessage = complaint.ComplaintMessage,
                                    ComplaintId = complaint.Id,
                                    ComplaintUserName = citiizen.Name
                                });
                            }
                        }
                    }
                }

            }

            return citizensResponse;
        }

        public List<Users> GetAllUsers()
        {
            client = new HttpClient();
            var res = new List<Users>();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Users/.js").Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsStringAsync().Result;
                var allUsers = JsonConvert.DeserializeObject<List<Users>>(user);

                if (allUsers != null)
                {
                    foreach (var singleUser in allUsers)
                    {
                        if (singleUser.UserType == 1)
                        {
                            res.Add(singleUser);
                        }
                    }
                }
            }
            else
            {
                res = default;
            }
            return res;
        }

        public List<JobVacencies> GetApprovalPendingVacanciesList()
        {
            client = new HttpClient();
            var res = new List<JobVacencies>();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Vacancies/GetApprovalPendingVacancies.js").Result;
            if (response.IsSuccessStatusCode)
            {
                var jobVacancies = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<List<JobVacencies>>(jobVacancies);
            }

            return res;
        }

        public Citizen GetCitizenById(string id)
        {
            client = new HttpClient();
            var res = new Citizen();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Citizens/GetCitizenById/"+ id +".js").Result;
            if (response.IsSuccessStatusCode)
            {
                var complaints = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<Citizen>(complaints);
            }

            return res;
        }

        public DetailedCitizenResponse GetCitizensFullDetailsByCitizenId(string citizenId)
        {
            client = new HttpClient();
            var res = new DetailedCitizenResponse();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Citizens/GetDeatailCitizen/" + citizenId + ".js").Result;
            if (response.IsSuccessStatusCode)
            {
                var citizenDetails = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<DetailedCitizenResponse>(citizenDetails);
            }

            return res;
        }

        public Complaint GetComplaintByCitizenId(string id)
        {
            client = new HttpClient();
            var res = new Complaint();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Complaint/GetByUserId/" + id + ".js").Result;
            if (response.IsSuccessStatusCode)
            {
                var complaints = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<Complaint>(complaints);
            }

            return res;
        }

        public List<DetailedCitizenResponse> GetDetailCitizensPerVacancy(string vacancyId)
        {
            client = new HttpClient();
            var detailedCitizensList = new List<DetailedCitizenResponse>();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Vacancies/GetAllCandidatesPerVacancy/" + vacancyId + ".js").Result;
            if (response.IsSuccessStatusCode)
            {
                var complaints = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<List<Citizen>>(complaints);

                if (res != null)
                {
                    foreach (var citizen in res)
                    {
                        var detailedObject = GetCitizensFullDetailsByCitizenId(citizen.Id);

                        if (detailedObject != null)
                        {
                            detailedCitizensList.Add(detailedObject);
                        }
                    }
                }
            }

            return detailedCitizensList;
        }

        public List<JobVacencies> GetVacanciesByCompany(string companyId)
        {
            client = new HttpClient();
            var res = new List<JobVacencies>();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Vacancies/GetVacanciesPerCompany/" + companyId + ".js").Result;
            if (response.IsSuccessStatusCode)
            {
                var complaints = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<List<JobVacencies>>(complaints);
            }

            return res;
        }

        public async Task<bool> ReplyOnComplaint(string complaintId, string replyMessage)
        {
            var complainReplyMessage = new ReplyToComplainRequest()
            {
                ComplainId = complaintId,
                Reply = replyMessage,
            };

            var company = JsonConvert.SerializeObject(complainReplyMessage);
            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:8001/api/Complaint/ReplyToComplain.js", requestContent);

            if (response.IsSuccessStatusCode)
            { return true; }

            return false;
        }

        public async Task<bool> UpdateVacancy(UpdateVacancyRequest request)
        {
            var company = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:8001/api/Vacancies/UpdateVacancy.js", requestContent);

            if (response.IsSuccessStatusCode)
            { return true; }

            return false;
        }

        public async Task<Users> ValidateUserCredentials(LoginDetails loginDetails)
        {
            client = new HttpClient();
            var res = new Users();

            client.BaseAddress = new Uri("http://localhost:8001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Users/VerifyUser/" + loginDetails.Email + "/" + loginDetails.Password + ".js").Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<Users>(user);
            }
            else
            {
                res = default;
            }
            return res;
        }

        public async Task<bool> VerifyCitizen(string citizenNic)
        {
            client = new HttpClient();
            var res = false;

            client.BaseAddress = new Uri("http://localhost:7050/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            VerifyInformationRequest requestData = new () {
                IsValid = true,
            };
            try {

                HttpResponseMessage response = await client.PutAsJsonAsync("api/Citizens/VerifyInformation/" + citizenNic + " .js", requestData).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var user = response.Content.ReadAsStringAsync().Result;
                    res = JsonConvert.DeserializeObject<bool>(user);
                }
                else
                {
                    res = false;
                }

                return res;

            } catch (Exception ex)
            {
                return res;
            }
            
        }

    }
}

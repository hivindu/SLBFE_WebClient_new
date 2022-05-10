using SLBFE_WebClient.Models;

namespace SLBFE_WebClient.Services.Interfaces
{
    public interface ISLBFE_Services
    {
        List<Citizen> GetAllCitizens();
        List<Users> GetAllUsers();
        Citizen GetCitizenById(string id);
        Task<bool> ChangeCompanyActivationStatus(string companyId, bool status);
        Task<bool> ChangeCitizenActivationStatus(string citizenId, bool status);
        List<ComplaintViewResponse> GetAllComplaints();
        List<Users> GetAllCompanies();
        DetailedCitizenResponse GetCitizensFullDetailsByCitizenId(string citizenId);
        Complaint GetComplaintByCitizenId(string id);
        List<JobVacencies> GetApprovalPendingVacanciesList();
        List<JobVacencies> GetVacanciesByCompany(string companyId);
        JobVacencies CreateVacancy(CreateVacancyRequest vacancyRequest);
        Users CreateCompanyAccount(CreateCompanyUserRequest request);
        Users CreateUserAccount(CreateUserRequest request);
        Task<bool> ApproveVacancy(string vacancyid);
        Task<bool> VerifyCitizen(string citizenNic);
        Task<bool> ReplyOnComplaint(string complaintId, string replyMessage);
        Task<bool> UpdateVacancy(UpdateVacancyRequest request);
        Task<Users> ValidateUserCredentials(LoginDetails loginDetails);
        List<DetailedCitizenResponse> GetDetailCitizensPerVacancy(string vacancyId);
        bool DeleteVacancy(string id);

        bool DeleteUser(string id);
    }
}

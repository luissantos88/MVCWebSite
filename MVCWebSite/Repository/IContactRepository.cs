using MVCWebSite.Models;

namespace MVCWebSite.Repository
{
    public interface IContactRepository
    {
        List<ContactModel> GetAll(int userId);
        ContactModel ListById(int id);
        ContactModel Add(ContactModel contact);
        ContactModel Update(ContactModel contact);
        bool Delete(int id);
    }
}

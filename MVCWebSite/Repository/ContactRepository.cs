using MVCWebSite.Data;
using MVCWebSite.Models;

namespace MVCWebSite.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly BankContext _bankContext;
        public ContactRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        public ContactModel ListById(int id)
        {   
            return _bankContext.Contacts.FirstOrDefault(x => x.Id == id);
        }

        public List<ContactModel> GetAll(int userId)
        {
            return _bankContext.Contacts.Where(x => x.UserId == userId).ToList();
        }
        public ContactModel Add(ContactModel contact)
        {
            // save in data base
            _bankContext.Contacts.Add(contact);
            _bankContext.SaveChanges();
            return contact;
        }
        public ContactModel Update(ContactModel contact)
        {
            ContactModel contactDB = ListById(contact.Id);
            if(contactDB == null)
            {
                throw new Exception("Something went error updating this user");
            }
            contactDB.Name = contact.Name;
            contactDB.Email = contact.Email;
            contactDB.Phone = contact.Phone;

            _bankContext.Contacts.Update(contactDB);
            _bankContext.SaveChanges();
            return contactDB;
        }

        public bool Delete(int id)
        {
            ContactModel contactDB = ListById(id);
            if (contactDB == null)
            {
                throw new Exception("Something went error deleting this user");
            }

            _bankContext.Contacts.Remove(contactDB);
            _bankContext.SaveChanges();

            return true;
        }
    }
}

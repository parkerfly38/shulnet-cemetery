using cemetery.Models;

namespace cemetery.Data
{
    public interface IDataRepository
    {
        #region Interment
        public Task<IEnumerable<Internment>> GetInternments();
        public Task<Internment> GetInternment(int id);
        public bool AddInternment(Internment internment);
        public bool UpdateInternment(Internment internment);
        public bool DeleteInternment(int id);
        #endregion

        #region Section
        public Task<IEnumerable<Section>> GetSections();
        public Task<Section> GetSection(int id);
        public bool AddSection(Section section);
        public bool UpdateSection(Section section);
        public bool DeleteSection(int id);
        public Task<IEnumerable<Section>> GetSectionsForCemetery(int cemeteryid);
        #endregion

        #region Cemetery
        public Task<IEnumerable<Cemetery>> GetCemeteries();
        public Task<Cemetery> GetCemetery(int id);
        public bool AddCemetery(Cemetery cemetery);
        public bool UpdateCemetery(Cemetery cemetery);
        public bool DeleteCemetery(int id);
        #endregion

        #region Deed
        public Task<IEnumerable<Deed>> GetDeeds();
        public Task<Deed> GetDeed(int id);
        public bool AddDeed(Deed deed);
        public bool UpdateDeed(Deed deed);
        public bool DeleteDeed(int id);
        public Task<IEnumerable<Deed>> SearchDeeds(string searchterm);
        #endregion

        #region Person
        public Task<IEnumerable<Person>> GetPeople();
        public Task<Person> GetPerson(int id);
        public bool AddPerson(Person person);
        public bool UpdatePerson(Person person);
        public bool DeletePerson(int id);
        #endregion

        #region Users
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int id);
        public bool AddUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(int id);
        #endregion

        #region Roles
        public Task<IEnumerable<Role>> GetRoles();
        public Task<Role> GetRole(int id);
        public bool AddRole(Role role);
        public bool UpdateRole(Role role);
        public bool DeleteRole(int id);
        public bool AddUserRole(int roleid, int userid);
        #endregion
    }
}
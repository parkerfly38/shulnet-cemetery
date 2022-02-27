using cemetery.Models;
using Dapper;


namespace cemetery.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<DataRepository> _logger;

        public DataRepository(ILogger<DataRepository> logger, DataContext context)
        {
            _context = context;
            _logger = logger;
        }

        #region Internments
        public async Task<IEnumerable<Internment>> GetInternments()
        {
            string sql = @"SELECT * FROM internment";
            using (var connection = _context.CreateConnection())
            {
                var internments = await connection.QueryAsync<Internment>(sql);
                return internments.ToList();
            }
        }

        public async Task<Internment> GetInternment(int id)
        {
            string sql = @"SELECT * FROM internment WHERE id = @id";
            using (var connection = _context.CreateConnection())
            {
                var internment = await connection.QuerySingleOrDefaultAsync<Internment>(sql, new { id });
                if (internment != null)
                {
                    // add section
                    internment.Section = await GetSection(internment.SectionId);
                    // add Cemetery
                    internment.Cemetery = await GetCemetery(internment.CemeteryId);
                }
                return internment;
            }
        }

        public bool AddInternment(Internment internment)
        {
            string sql = @"INSERT INTO internment (sectionid, lot, book, pagenumber, deceaseddate, firstname, lastname, middlename, birthplace, lastresidence, age, sex, cemeteryid, notes, lot2)
                            VALUES (@sectionid, @lot, @book, @pagenumber, @deceaseddate, @firstname, @lastname, @middlename, @birthplace, @lastresidence, @age, @sex, @cemeteryid, @notes, @lot2);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("sectionid", internment.SectionId);
            parameters.Add("lot", internment.Lot);
            parameters.Add("book", internment.Book);
            parameters.Add("pagenumber",internment.PageNumber);
            parameters.Add("deceaseddate", internment.DeceasedDate);
            parameters.Add("firstname", internment.FirstName);
            parameters.Add("lastname", internment.LastName);
            parameters.Add("middlename", internment.MiddleName);
            parameters.Add("birthplace", internment.BirthPlace);
            parameters.Add("lastresidence", internment.LastResidence);
            parameters.Add("age", internment.Age);
            parameters.Add("sex", internment.Sex);
            parameters.Add("cemeteryid", internment.CemeteryId);
            parameters.Add("notes", internment.Notes);
            parameters.Add("lot2", internment.Lot2);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                }
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddInternment: {0}", exception.Message);
                return false;
            }
        }

        public bool UpdateInternment(Internment internment)
        {
            string sql = @"UPDATE internment
                SET sectionid = @sectionid,
                    lot = @lot,
                    book = @book,
                    pagenumber = @pagenumber,
                    deceaseddate = @deceaseddate,
                    firstname = @firstname,
                    lastname = @lastname,
                    middlename = @middlename,
                    birthplace = @birthplace,
                    lastresidence = @lastresidence,
                    age = @age,
                    sex = @sex,
                    cemeteryid = @cemeteryid,
                    notes = @notes,
                    lot2 = @lot2
                    WHERE id = @id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("sectionid", internment.SectionId);
            parameters.Add("lot", internment.Lot);
            parameters.Add("book", internment.Book);
            parameters.Add("pagenumber",internment.PageNumber);
            parameters.Add("deceaseddate", internment.DeceasedDate);
            parameters.Add("firstname", internment.FirstName);
            parameters.Add("lastname", internment.LastName);
            parameters.Add("middlename", internment.MiddleName);
            parameters.Add("birthplace", internment.BirthPlace);
            parameters.Add("lastresidence", internment.LastResidence);
            parameters.Add("age", internment.Age);
            parameters.Add("sex", internment.Sex);
            parameters.Add("cemeteryid", internment.CemeteryId);
            parameters.Add("notes", internment.Notes);
            parameters.Add("lot2", internment.Lot2);
            parameters.Add("id", internment.id);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                }
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.UpdateInternment: {0}", exception.Message);
                return false;
            }
        }
    
        public bool DeleteInternment(int id)
        {
            string sql = @"DELETE FROM internment WHERE id = @id";
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new { id });
                }
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.DeleteInternment: {0}", exception.Message);
                return false;
            }
        }
        #endregion

        #region Section
        public async Task<IEnumerable<Section>> GetSections()
        {
            string sql = @"SELECT * FROM section;";
            using (var connection = _context.CreateConnection())
            {
                var sections = await connection.QueryAsync<Section>(sql);
                return sections.ToList();
            }
        }

        public async Task<Section> GetSection(int id)
        {
            string sql = @"SELECT * from section WHERE id = @id";
            using (var connection = _context.CreateConnection())
            {
                var section = await connection.QuerySingleOrDefaultAsync<Section>(sql, new { id });
                if (section != null)
                {
                    var cemetery = await GetCemetery(section.CemeteryId);
                }
                return section;
            }
        }

        public bool AddSection(Section section)
        {
            string sql = @"INSERT INTO section (cemeteryid, code, name) VALUES (@cemeteryid, @code, @name);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("cemeteryid", section.CemeteryId);
            parameters.Add("code", section.Code);
            parameters.Add("name", section.Name);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                }
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddSection: {0}", exception.Message);
                return false;
            }
        }

        public bool UpdateSection(Section section)
        {
            string sql = @"UPDATE section
                            SET cemeteryid = @cemeteryid,
                                code = @code,
                                name = @name
                            WHERE id = @id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("cemeteryid", section.CemeteryId);
            parameters.Add("code", section.Code);
            parameters.Add("name", section.Name);
            parameters.Add("id", section.id);
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.UpdateSection: {0}", exception.Message);
                return false;
            }
        }

        public bool DeleteSection(int id)
        {
            string sql = @"DELETE FROM section WHERE id = @id;";
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new {id});
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.DeleteSection: {0}", exception.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Section>> GetSectionsForCemetery(int cemeteryid)
        {
            string sql = @"SELECT * FROM Section WHERE cemeteryid = @cemeteryid;";
            using (var connection = _context.CreateConnection())
            {
                var sections = await connection.QueryAsync<Section>(sql, new { cemeteryid });
                return sections.ToList();
            }
        }
        #endregion

        #region Cemetery
        public async Task<IEnumerable<Cemetery>> GetCemeteries()
        {
            string sql = @"SELECT * FROM cemetery;";
            using (var connection = _context.CreateConnection())
            {
                var cemeteries = await connection.QueryAsync<Cemetery>(sql);
                return cemeteries.ToList();
            }
        }

        public async Task<Cemetery> GetCemetery(int id)
        {
            string sql = @"SELECT * FROM cemetery WHERE id = @id;";
            using (var connection = _context.CreateConnection())
            {
                var cemetery = await connection.QuerySingleOrDefaultAsync<Cemetery>(sql, new { id });
                return cemetery;
            }
        }

        public bool AddCemetery(Cemetery cemetery)
        {
            string sql = @"INSERT INTO cemetery (name, googlemapurl) VALUES (@name, @googlemapurl);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", cemetery.Name);
            parameters.Add("googlemapurl", cemetery.GoogleMapUrl);
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddCemetery: {0}", exception.Message);
                return false;
            }
        }

        public bool UpdateCemetery(Cemetery cemetery)
        {
            string sql = @"UPDATE cemetery SET name = @name, googlemapurl = @googlemapurl WHERE id = @id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", cemetery.Name);
            parameters.Add("googlemapurl", cemetery.GoogleMapUrl);
            parameters.Add("id", cemetery.id);
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.UpdateCemetery: {0}", exception.Message);
                return false;
            }
        }

        public bool DeleteCemetery(int id)
        {
            string sql = @"DELETE FROM cemetery WHERE id = @id;";
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new { id});
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.DeleteCemetery: {0}", exception.Message);
                return false;
            }
        }
        #endregion

        #region Deed
        public async Task<IEnumerable<Deed>> GetDeeds()
        {
            string sql = "SELECT * FROM deed;";
            using (var connection = _context.CreateConnection())
            {
                var deeds = await connection.QueryAsync<Deed>(sql);
                return deeds.ToList();
            }
        }

        public async Task<Deed> GetDeed(int id)
        {
            string sql = "SELECT * FROM deed WHERE id = @id;";
            using (var connection = _context.CreateConnection())
            {
                var deed = await connection.QuerySingleOrDefaultAsync<Deed>(sql, new { id });
                if (deed != null)
                {
                    deed.Section = await GetSection(deed.SectionId);
                    deed.Cemetery = await GetCemetery(deed.CemeteryId);
                }
                return deed;
            }
        }

        public bool AddDeed(Deed deed)
        {
            string sql = @"INSERT INTO deed (sectionid, lot, lastname1, firstname1, middlename1, lastname2, firstname2, middlename2, issuedate, notes, cemeteryid)
                            VALUES (@sectionid, @lot, @lastname1, @firstname1, @middlename1, @lastname2, @firstname2, @middlename2, @issuedate, @notes, @cemeteryid);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("sectionid", deed.SectionId);
            parameters.Add("lot", deed.Lot);
            parameters.Add("lastname1", deed.LastName1);
            parameters.Add("firstname1", deed.FirstName1);
            parameters.Add("middlename1", deed.MiddleName1);
            parameters.Add("lastname2", deed.LastName2);
            parameters.Add("firstname2", deed.FirstName2);
            parameters.Add("middlename2", deed.MiddleName2);
            parameters.Add("issuedate", deed.IssueDate);
            parameters.Add("notes", deed.Notes);
            parameters.Add("cemeteryid", deed.CemeteryId);
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddDeed: {0}", exception.Message);
                return false;
            }
        }

        public bool UpdateDeed(Deed deed)
        {
            string sql = @"UPDATE deed
                           SET  sectionid = @sectionid,
                                lot = @lot,
                                lastname1 = @lastname1,
                                firstname1 = @firstname1,
                                middlename1 = @middlename1,
                                lastname2 = @lastname2,
                                firstname2 = @firstname2,
                                middlename2 = @middlename2,
                                issuedate = @issuedate,
                                notes = @notes,
                                cemeteryid = @cemeteryid
                            WHERE id = @id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("sectionid", deed.SectionId);
            parameters.Add("lot", deed.Lot);
            parameters.Add("lastname1", deed.LastName1);
            parameters.Add("firstname1", deed.FirstName1);
            parameters.Add("middlename1", deed.MiddleName1);
            parameters.Add("lastname2", deed.LastName2);
            parameters.Add("firstname2", deed.FirstName2);
            parameters.Add("middlename2", deed.MiddleName2);
            parameters.Add("issuedate", deed.IssueDate);
            parameters.Add("notes", deed.Notes);
            parameters.Add("cemeteryid", deed.CemeteryId);
            parameters.Add("id", deed.id);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.UpdateDeed: {0}", exception.Message);
                return false;
            }
        }

        public bool DeleteDeed(int id)
        {
            string sql = @"DELETE FROM deed WHERE id = @id;";
            try 
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new { id });
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.DeleteDeed: {0}", exception.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Deed>> SearchDeeds(string searchterm)
        {
            string sql = @"SELECT * FROM deed 
                    WHERE lastname1 ILIKE CONCAT('%',@searchterm,'%')
                    OR firstname1  ILIKE CONCAT('%',@searchterm,'%')
                    OR middlename1  ILIKE CONCAT('%',@searchterm,'%')
                    OR lastname2 ILIKE CONCAT('%',@searchterm,'%')
                    OR firstname2  ILIKE CONCAT('%',@searchterm,'%')
                    OR middlename2  ILIKE CONCAT('%',@searchterm,'%')
                    OR notes  ILIKE CONCAT('%',@searchterm,'%');";
            using (var connection = _context.CreateConnection())
            {
                var deeds = await connection.QueryAsync<Deed>(sql, new { searchterm });
                return deeds.ToList();
            }
        }
        #endregion
    
        #region Person
        public async Task<IEnumerable<Person>> GetPeople()
        {
            string sql = "SELECT * FROM person;";
            using (var connection = _context.CreateConnection())
            {
                var people = await connection.QueryAsync<Person>(sql);
                return people.ToList();
            }
        }

        public async Task<Person> GetPerson(int id)
        {
            string sql = "SELECT * FROM person WHERE id = @id;";
            using (var connection = _context.CreateConnection())
            {
                var person = await connection.QuerySingleOrDefaultAsync<Person>(sql, new { id });
                if (person != null)
                {
                    person.User = await GetUser(person.UserId);
                }
                return person;
            }
        }

        public bool AddPerson(Person person)
        {
            string sql = @"INSERT INTO person (firstname, lastname, email, userid) VALUES (@firstname, @lastname, @email, @userid);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("firstname", person.FirstName);
            parameters.Add("lastname", person.LastName);
            parameters.Add("email", person.Email);
            parameters.Add("userid", person.UserId);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddPerson: {0}", exception.Message);
                return false;
            }
        }

        public bool UpdatePerson(Person person)
        {
            string sql = @"UPDATE person SET firstname = @firstname, lastname = @lastname, email = @email, userid = @userid WHERE id = @id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("firstname", person.FirstName);
            parameters.Add("lastname", person.LastName);
            parameters.Add("email", person.Email);
            parameters.Add("userid", person.UserId);
            parameters.Add("id", person.id);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.UpdatePerson: {0}", exception.Message);
                return false;
            }
        }

        public bool DeletePerson(int id)
        {
            string sql = @"DELETE FROM person WHERE id = @id;";
            string sql2 = "DELETE FROM userdata WHERE personid = @id;";
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new { id });
                    connection.Execute(sql2, new { id });
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.DeletePerson: {0}", exception.Message);
                return false;
            }
        }
        #endregion
    
        #region User
        public async Task<IEnumerable<User>> GetUsers()
        {
            string sql = "SELECT * FROM userdata;";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(sql);
                return users.ToList();
            }
        }

        public async Task<User> GetUser(int id)
        {
            string sql = "SELECT * FROM userdata WHERE id = @id;";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { id });
                if (user != null)
                {
                    user.Person = await GetPerson(user.PersonId);
                    // get roles
                    string roleSql = "SELECT role.id, role.name FROM role INNER JOIN userroles ON roleid = role.id WHERE userid = @id;";
                    var roles = await connection.QueryAsync<Role>(sql, new { user.id });
                    user.Roles = roles.ToList();
                }
                return user;
            }
        }

        public bool AddUser(User user)
        {
            string sql = @"INSERT INTO userdata (personid, username, password, authenticationmethod)
                            VALUES (@personid, @username, @password, @authenticationmethod);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("personid", user.PersonId);
            parameters.Add("username", user.UserName);
            parameters.Add("password", user.Password);
            parameters.Add("authenticationmethod", user.AuthenticationMethod);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddUser: {0}", exception.Message);
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            string sql = @"UPDATE userdata
                           SET  personid = @personid,
                                username = @username,
                                password = @password,
                                authenticationmethod = @authenticationmethod
                            WHERE id = @id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("personid", user.PersonId);
            parameters.Add("username", user.UserName);
            parameters.Add("password", user.Password);
            parameters.Add("authenticationmethod", user.AuthenticationMethod);
            parameters.Add("id", user.id);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.UpdateUser: {0}", exception.Message);
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            string sql = "DELETE FROM userdata WHERE id = @id;";
            string sql2 = "DELETE FROM person WHERE userid = @id;";
            string sql3 = "DELETE FROM userroles WHERE userid = @id;";
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new { id });
                    connection.Execute(sql2, new { id });
                    connection.Execute(sql3, new { id });
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.DeleteUser: {0}", exception.Message);
                return false;
            }
        }
        #endregion

        #region Role
        public async Task<IEnumerable<Role>> GetRoles()
        {
            string sql = "SELECT * FROM role;";
            using (var connection = _context.CreateConnection())
            {
                var roles = await connection.QueryAsync<Role>(sql);
                return roles.ToList();
            }
        }

        public async Task<Role> GetRole(int id)
        {
            string sql = "SELECT * FROM role WHERE id = @id;";
            using (var connection = _context.CreateConnection())
            {
                var role = await connection.QuerySingleOrDefaultAsync<Role>(sql, new { id } );
                return role;
            }
        }

        public bool AddRole(Role role)
        {
            string sql = "INSERT INTO role (name) VALUES (@name);";
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new { role.Name });
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddRole: {0}", exception.Message);
                return false;
            }
        }

        public bool UpdateRole(Role role)
        {
            string sql = "UPDATE role SET name = @name WHERE id = @id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", role.Name);
            parameters.Add("id", role.id);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.UpdateRole: {0}", exception.Message);
                return false;
            }
        }

        public bool DeleteRole(int id)
        {
            string sql = "DELETE FROM role WHERE id = @id;";
            //we also need to delete from roles
            string sql2 = "DELETE FROM userroles WHERE roleid = @id;";
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, new { id });
                    connection.Execute(sql2, new { id });
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.DeleteRole: {0}", exception.Message);
                return false;
            }
        }

        public bool AddUserRole(int roleid, int userid)
        {
            string sql = "INSERT INTO userroles (userid, roleid) VALUES (@userid, @roleid);";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("roleid", roleid);
            parameters.Add("userid", userid);
            try {
                using (var connection = _context.CreateConnection())
                {
                    connection.Execute(sql, parameters);
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "DataRepository.AddUserRole: {0}", exception.Message);
                return false;
            }
        }
        #endregion
    }
}
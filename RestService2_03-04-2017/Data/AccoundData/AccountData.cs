using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using RestService.Models;
using RestService.BLL;
using RestService.CommService;

namespace RestService
{
    public class AccountData
    {
        // Поиск аккаунта по номеру телефона при подключении клиентского Приложения
        public static Account SqlFindAccount(string phoneNumber, string phoneCode = "7", int language = 0)
        {
            Account account = new Account();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindAccount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar, 50).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar, 10).Value = phoneCode;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        account.ID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        account.FirstName = (reader["first_name"] != DBNull.Value) ? Convert.ToString(reader["first_name"]) : "";
                        account.LastName = (reader["last_name"] != DBNull.Value) ? Convert.ToString(reader["last_name"]) : "";
                        account.PhoneModel = (reader["phone_model"] != DBNull.Value) ? Convert.ToString(reader["phone_model"]) : "";
                        account.PhoneCode = (reader["phone_code"] != DBNull.Value) ? Convert.ToString(reader["phone_code"]) : "7";
                        account.PhoneNumber = (reader["phone_number"] != DBNull.Value) ? Convert.ToString(reader["phone_number"]) : "";
                        account.PhoneUID = (reader["phone_uid"] != DBNull.Value) ? Convert.ToString(reader["phone_uid"]) : "";
                        account.Email = (reader["email"] != DBNull.Value) ? Convert.ToString(reader["email"]) : "";
                        PhoneOS phone_os = new PhoneOS();
                        phone_os.ID = (reader["os_id"] != DBNull.Value) ? Convert.ToInt32(reader["os_id"]) : 0;
                        phone_os.Name = (reader["os_name"] != DBNull.Value) ? Convert.ToString(reader["os_name"]) : "";
                        account.PhoneOS = phone_os;
                        account.Pswd = (reader["pswd"] != DBNull.Value) ? Convert.ToString(reader["pswd"]) : "";
                        AccountBlock block = new AccountBlock();
                        block.ID = (reader["block_id"] != DBNull.Value) ? Convert.ToInt32(reader["block_id"]) : 0;
                        block.Name = (reader["block_id"] != DBNull.Value) ? Convert.ToString(reader["block_name"]) : "";
                        account.AccountBlock = block;
                        AccountStatus status = new AccountStatus();
                        status.ID = (reader["status_id"] != DBNull.Value) ? Convert.ToInt32(reader["status_id"]) : 0;
                        status.Name = (reader["status_id"] != DBNull.Value) ? Convert.ToString(reader["status_name"]) : "";
                        account.AccountStatus = status;
                        XMLGenerator<Account> accountXML = new XMLGenerator<Account>(account);
                        Helper.saveToLog(0, "", "SqlFindAccount", "phoneNumber=" + phoneNumber, "Аккаунт найден: " + accountXML.GetStringXML(), 0);
                        return account;
                    }
                    else
                    {
                        //account.Message = "Аккаунт не найден.";
                        account.Message = Helper.GetError(7, language);
                        Helper.saveToLog(0, "", "SqlFindAccount", "phoneNumber=" + phoneNumber, "Аккаунт не найден.", 1);
                        return account;
                    }
                }
            }
            catch (Exception e)
            {
                //account.Message = "Внутренняя ошибка сервиса: " + e.Message;
                account.Message = Helper.GetError(100, language);
                Helper.saveToLog(0, "", "SqlFindAccount", "phoneNumber=" + phoneNumber, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return account;
            }
        }

        //Поиск clientId по номеру телефона
        public static string SqlFindClientId(string phoneNumber, string phoneCode = "7")
        {
            if (phoneCode == "") { phoneCode = "7"; }
            string ret = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindAccountID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar, 50).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar, 10).Value = phoneCode;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = (reader["id"] != DBNull.Value) ? (reader["id"]).ToString() : "";
                    }
                }
            }
            catch
            {
            }
            return ret;
        }

        // Регистрация нового аккаунта
        public static Account SqlRegistrAccount(Account account, int language)
        {
            string param = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    Helper hlp = new Helper();
                    //SqlCommand cmd = new SqlCommand("Rest.dbo.RegistrAccount", con);
                    SqlCommand cmd = new SqlCommand("Rest.dbo.RegistrAccount2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = account.FirstName;
                    cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = account.LastName;
                    cmd.Parameters.Add("@phone_model", SqlDbType.VarChar).Value = account.PhoneModel;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = account.PhoneCode;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = account.PhoneNumber;
                    cmd.Parameters.Add("@phone_uid", SqlDbType.VarChar).Value = account.PhoneUID;
                    int uid = (account.PhoneOS != null) ? account.PhoneOS.ID : 0;
                    cmd.Parameters.Add("@phone_os", SqlDbType.Int).Value = uid;
                    account.SMScode = hlp.CreateRandomSMS();
                    cmd.Parameters.Add("@smscode", SqlDbType.VarChar).Value = account.SMScode;
                    cmd.Parameters.Add("@pswd", SqlDbType.VarChar).Value = account.Pswd;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = account.Email;
                    cmd.Parameters.Add("@status_id", SqlDbType.Int).Value = 1;
                    con.Open();
                    object oIdent = DBNull.Value;
                    oIdent = cmd.ExecuteScalar();
                    int pid = (oIdent != DBNull.Value) ? Convert.ToInt32(oIdent) : -1;
                    account.ID = pid;
                    param = "@first_name=" + account.FirstName + ", @last_name=" + account.LastName + ", @phone_model=" + account.PhoneModel + ", @phone_code=" + account.PhoneCode + ", @phone_number=" + account.PhoneNumber + ", @phone_uid=" + account.PhoneUID + ", @phone_os=" + uid + ", @smscode=" + account.SMScode + ", @pswd=" + account.Pswd + ", @email=" + account.Email + ", @status_id=2";
                    XMLGenerator<Account> accountXML = new XMLGenerator<Account>(account);
                    Helper.saveToLog(account.ID, "", "RegistrAccount", "param: " + param, "Аккаунт зарегистрирован: " + accountXML.GetStringXML(), 0);
                    //запись в табл. RegistrationLog
                    SqlCommand cmd2 = new SqlCommand("Rest.dbo.RegistrLog", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@account_id", SqlDbType.Int).Value = account.ID;
                    cmd2.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = account.PhoneNumber;
                    cmd2.Parameters.Add("@phoneCode", SqlDbType.VarChar).Value = account.PhoneCode;
                    cmd2.Parameters.Add("@sms_code", SqlDbType.VarChar).Value = account.SMScode;
                    cmd2.Parameters.Add("@action", SqlDbType.VarChar).Value = "registration";
                    cmd2.ExecuteNonQuery();

                    //отправка SMS c account.SMScode
                    SmsServiceSoapClient comm = new SmsServiceSoapClient();
                    string sessionID = comm.GetSessionID("novatorov", "VeeP2016");
                    //decimal balance = comm.GetBalance(sessionID);
                    //string[] result = comm.SendMessageByTimeZone(sessionID,"TESTSMS","89153902665","1234",DateTime.Now,10);
                    Message msg = new Message();
                    msg.Data = DateTime.Now.ToString();
                    msg.SourceAddress = "VEEP";
                    msg.DestinationAddresses = new string[] { "+" + account.PhoneCode + account.PhoneNumber };
                    msg.Data = "Ваш код: " + account.SMScode;
                    msg.Validity = 10;
                    string[] result = comm.SendMessage(sessionID, msg);
                }
                Helper.saveToAccountLog(account.ID, "", "Аккаунт зарегистрирован. Статус: неактивен. SMS с кодом отправлена.", "");
                return account;
            }
            catch (Exception e)
            {
                //account.Message = "Внутренняя ошибка сервиса: " + e.Message;
                account.Message = Helper.GetError(2, language);
                Helper.saveToLog(0, "", "SqlRegistrAccount", "param: " + param, "Внутренняя ошибка сервиса: " + e.Message, 1);
                Helper.saveToAccountLog(account.ID, "", "Ошибка при регистрации аккаунта.", "Ошибка: " + e.Message);
                return account;
            }
        }

        //Проверка введенного кода SMS
        public static bool SqlCheckSMS(string phoneNumber, string phoneCode, string SMScode)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.CheckSMS", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phoneCode", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@sms_code", SqlDbType.VarChar).Value = SMScode;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int ret = (int)reader[0];
                        if (ret > 0)
                        {
                            Helper.saveToLog(0, "", "CheckSMS", "phoneNumber=" + phoneNumber + ", SMScode=" + SMScode, "SMS код введен правильно", 0);
                            return true;
                        }
                        else
                        {
                            Helper.saveToLog(0, "", "CheckSMS", "phoneNumber=" + phoneNumber + ", SMScode=" + SMScode, "Ошибка при вводе SMS кода", 1);
                            return false;
                        }
                    }
                    else
                    {
                        Helper.saveToLog(0, "", "CheckSMS", "phoneNumber=" + phoneNumber + ", SMScode=" + SMScode, "Ошибка при вводе SMS кода", 1);
                        return false;
                    }

                }
            }
            catch (Exception e)
            {
                string msg = "Внутренняя ошибка сервиса: " + e.Message;
                Helper.saveToLog(0, "", "SqlCheckSMS", "phoneNumber=" + phoneNumber + ", SMScode=" + SMScode, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return false;
            }

        }

        //Изменение статуса аккаунта
        public static int SqlUpdateStatus(int accountID, int statusID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.UpdateAccountStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@accountID", SqlDbType.Int).Value = accountID;
                    cmd.Parameters.Add("@statusID", SqlDbType.Int).Value = statusID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(accountID, "", "UpdateStatus", "accountID: " + accountID.ToString() + ", statusID= " + statusID.ToString(), "Изменение статуса успешно", 0);
                    Helper.saveToAccountLog(accountID, "", "Изменен статус аккаунта. Новый статус: " + statusID, "");
                    return accountID;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(accountID, "", "UpdateStatus", "accountID: " + accountID.ToString() + ", statusID= " + statusID.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
                Helper.saveToAccountLog(accountID, "", "Ошибка при изменении статуса аккаунта.", "Ошибка: " + e.Message);
                return 0;
            }

        }

        // Проверка логина/пароля
        public static bool SqlLogin(string phoneNumber, string pswd, string phoneCode = "7")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.CheckLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@pswd", SqlDbType.VarChar).Value = pswd;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int accountID = (reader[0] != DBNull.Value)?(int)reader[0]:0;
                        if (accountID != 0)
                        {
                            Helper.saveToLog(accountID, "", "Login", "accountID: " + accountID.ToString() + ", phoneNumber= " + phoneNumber, "Авторизован.", 0);
                            Helper.saveToAccountLog(accountID, "", "Авторизован. Номер: " + phoneNumber, "");
                            return true;
                        }
                        else
                        {
                            Helper.saveToLog(null, "", "Login", "phoneNumber= " + phoneNumber, "Неправильный логин/пароль.", 1);
                            Helper.saveToAccountLog(null, "", "Неправильный логин/пароль. Номер: " + phoneNumber, "");
                            return false;
                        }
                    }
                    else
                    {
                        Helper.saveToLog(null, "", "Login", "phoneNumber= " + phoneNumber, "Неудачная авторизация.", 1);
                        Helper.saveToAccountLog(null, "", "Неудачная авторизация. Номер: " + phoneNumber, "");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(null, "", "Login", "phone_number: " + phoneNumber, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return false;
            }
        }

        // Проверка открытой сессии
        public static string SqlCheckSession(string phoneNumber, string phoneCode = "7")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.CheckSession", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        DateTime lastDate = (DateTime)reader["lastDate"];
                        if (lastDate <= DateTime.Now.AddHours(-4))  //4 часа
                        {
                            //закрываем сессию
                            AccountData.Exit(phoneNumber, phoneCode, null);
                            return "";
                        }
                        else
                        {
                            return reader["user_key"].ToString();
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        public static string SqlCheckKey(string user_key)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.CheckKey", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar).Value = user_key;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string phoneNumber = (string)reader["phone_number"];
                        DateTime lastDate = (DateTime)reader["lastDate"];
                        if (lastDate <= DateTime.Now.AddHours(-20))  //4 часа
                        {
                            //закрываем сессию
                            AccountData.Exit(null, null, user_key);
                            return "";
                        }
                        else
                        {
                            return phoneNumber;
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        // Открываем сессию
        public static string SqlInsertSession(string phoneNumber, string phoneCode = "7")
        {
            string user_key = Helper.getNewGUID();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertSession", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar, 32).Value = user_key;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(0, user_key, "SqlInsertSession", "phoneNumber: " + phoneNumber, "Сессия открыта", 0);
                    return user_key;
                }
            }
            catch
            {
                Helper.saveToLog(0, "", "SqlInsertSession", "phoneNumber: " + phoneNumber, "Не удалось открыть сессию", 1);
                return "";
            }
        }

        // Обновляем сессию
        public static int SqlUpdateSession(string phoneNumber)
        {
            int ret = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.UpdateSession", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ret = 1;
                    return ret;
                }
            }
            catch
            {
                return ret;
            }
        }

/*
        //Проверка открытой сессии
        public static string SqlSetSession(string phoneNumber)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("select top 1 user_key from dbo.AccountSession where phone_number=@phone_number order by lastDate desc", con);
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar, 20).Value = phoneNumber;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["user_key"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch
            {
                return "";
            }
        }*/

        // Полное удаление аккаунта аккаунта
        public static int DeleteAccount(string phoneNumber, string phoneCode = "7")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.DeleteAccount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Exit(phoneNumber,phoneCode, null);
                    Helper.saveToLog(0, "", "DeleteAccount", "phoneNumber: " + phoneNumber, "Аккаунт удален: phoneNumber=" + phoneNumber, 0);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "DeleteAccount", "phoneNumber: " + phoneNumber, "Ошибка при удалении аккаунта: " + e.Message, 1);
                return 0;
            }
        }

        // Блокировка аккаунта
        public static int BlockAccount(string phoneNumber, string phoneCode, int block_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.BlockAccount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@block_id", SqlDbType.Int).Value = block_id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Exit(phoneNumber, phoneCode,null);
                    Helper.saveToLog(0, "", "BlockAccount", "phoneNumber: " + phoneNumber, "Аккаунт заблокирован: block_id=" + block_id, 0);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "BlockAccount", "phoneNumber: " + phoneNumber, "Ошибка при блокировке аккаунта: " + e.Message, 1);
                return 0;
            }
        }

        //Обновление данных аккаунта
        public static Account SqlUpdateAccount(Account account, int language=0)
        {
            string param = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.UpdateAccount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@first_name", SqlDbType.VarChar).Value = account.FirstName;
                    cmd.Parameters.Add("@last_name", SqlDbType.VarChar).Value = account.LastName;
                    cmd.Parameters.Add("@phone_model", SqlDbType.VarChar).Value = account.PhoneModel;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = account.PhoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = account.PhoneCode;
                    cmd.Parameters.Add("@phone_uid", SqlDbType.VarChar).Value = account.PhoneUID;
                    cmd.Parameters.Add("@phone_os", SqlDbType.Int).Value = account.PhoneOS.ID;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = account.Email;
                    cmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account.ID;

                    param = "FirstName=" + account.FirstName + ", LastName=" + account.LastName + ", PhoneModel=" + account.PhoneModel + ", PhoneNumber=" + account.PhoneNumber + ", PhoneCode=" + account.PhoneCode + ", PhoneUID=" + account.PhoneUID + ", PhoneOS.ID=" + account.PhoneOS.ID.ToString() + ", Email=" + account.Email + ", AccountID = " + account.ID.ToString();

                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(account.ID, "", "SqlUpdateAccount", "param: " + param, "Аккаунт обновлен: ID = " + account.ID.ToString(), 0);
                }

                return account;
            }
            catch (Exception e)
            {
                //account.Message = "Внутренняя ошибка сервиса: " + e.Message;
                account.Message = Helper.GetError(100, language);
                Helper.saveToLog(account.ID, "", "SqlUpdateAccount", "param: " + param, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return account;
            }
        }

        //Смена пароля
        public static int SqlChangePsw(string phoneNumber, string phoneCode, string user_key, string oldPsw, string newPsw)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.ChangePsw", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@old_pswd", SqlDbType.VarChar).Value = oldPsw;
                    cmd.Parameters.Add("@new_pswd", SqlDbType.VarChar).Value = newPsw;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = (reader["account_id"] != DBNull.Value) ? 1 : 0;
                        Helper.saveToLog(null, user_key, "SqlChangePsw", "phoneNumber=" + phoneNumber + ", user_key=" + user_key + ", oldPsw=" + oldPsw + ", newPsw=" + newPsw, "Смена пароля прошла успешно", 0);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Helper.saveToLog(null, user_key, "SqlChangePsw", "phoneNumber=" + phoneNumber + ", user_key=" + user_key + ", oldPsw=" + oldPsw + ", newPsw=" + newPsw, "Внутренняя ошибка сервиса: " + e.Message, 1);
                 return result;
            }
        }


        // Закрываем сессию
        public static int Exit(string phoneNumber, string phoneCode, string user_key)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.CloseSession", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar).Value = user_key;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(0, "", "Exit", "phoneNumber: " + phoneNumber + ", user_key: " + user_key, "Сессия закрыта", 0);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "Exit", "phoneNumber: " + phoneNumber + ", user_key: " + user_key, "Ошибка при закрытии сессии: " + e.Message, 1);
                return 0;
            }
        }


        //Список всех аккаунтов (админка)
        public static List<Account> GetAccountsList(DateTime dfrom, DateTime dto)
        {
            List<Account> list = new List<Account>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetAccountsList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Account account = new Account();
                        account.ID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        account.FirstName = (reader["first_name"] != DBNull.Value) ? Convert.ToString(reader["first_name"]) : "";
                        account.LastName = (reader["last_name"] != DBNull.Value) ? Convert.ToString(reader["last_name"]) : "";
                        account.PhoneModel = (reader["phone_model"] != DBNull.Value) ? Convert.ToString(reader["phone_model"]) : "";
                        account.PhoneNumber = (reader["phone_number"] != DBNull.Value) ? Convert.ToString(reader["phone_number"]) : "";
                        account.PhoneUID = (reader["phone_uid"] != DBNull.Value) ? Convert.ToString(reader["phone_uid"]) : "";
                        account.Email = (reader["email"] != DBNull.Value) ? Convert.ToString(reader["email"]) : "";
                        PhoneOS phone_os = new PhoneOS();
                        phone_os.ID = (reader["os_id"] != DBNull.Value) ? Convert.ToInt32(reader["os_id"]) : 0;
                        phone_os.Name = (reader["os_name"] != DBNull.Value) ? Convert.ToString(reader["os_name"]) : "";
                        account.PhoneOS = phone_os;
                        account.Pswd = (reader["pswd"] != DBNull.Value) ? Convert.ToString(reader["pswd"]) : "";
                        AccountBlock block = new AccountBlock();
                        block.ID = (reader["block_id"] != DBNull.Value) ? Convert.ToInt32(reader["block_id"]) : 0;
                        block.Name = (reader["block_id"] != DBNull.Value) ? Convert.ToString(reader["block_name"]) : "";
                        account.AccountBlock = block;
                        AccountStatus status = new AccountStatus();
                        status.ID = (reader["status_id"] != DBNull.Value) ? Convert.ToInt32(reader["status_id"]) : 0;
                        status.Name = (reader["status_id"] != DBNull.Value) ? Convert.ToString(reader["status_name"]) : "";
                        account.AccountStatus = status;
                        if (reader["create_date"] != DBNull.Value)
                        {
                            account.CreateDate = (DateTime)reader["create_date"];
                        }
                        if (reader["update_date"] != DBNull.Value)
                        {
                            account.UpdateDate = (DateTime)reader["update_date"];
                        }
                        if (reader["last_date"] != DBNull.Value)
                        {
                            account.LastDate = (DateTime)reader["last_date"];
                        }
                        account.Cards = (reader["cards"] != DBNull.Value) ? Convert.ToString(reader["cards"]) : "";
                        account.Comment = (reader["comment"] != DBNull.Value) ? Convert.ToString(reader["comment"]) : "";

                        if (account != null)
                        {
                            list.Add(account);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return list;
        }

        //Изменение статуса аккаунта (админка)
        public static string ChangeAccountStatus(int id, int status_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.ChangeAccountStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@status_id", SqlDbType.Int).Value = status_id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return "Статус аккаунта успешно изменен";
                }
            }
            catch (Exception e)
            {
                return "Ошибка при изменении статуса аккаунта";
            }
        }

       
        //Полное удалени аккаунта (админка)
        public static string DeleteAccount(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.DeleteAccount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return "Аккаунт успешно деактивирован";
                }
            }
            catch (Exception e)
            {
                return "Ошибка при деактивации аккаунта";
            }
        }

        //Список новых аккаунтов для отчета (админка)
        public static List<ReportAccountNew> GetNewAccounts(DateTime dfrom, DateTime dto)
        {
            List<ReportAccountNew> list = new List<ReportAccountNew>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetAccountList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReportAccountNew new_account = new ReportAccountNew();
                        Account account = new Account();
                        account.ID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        account.FirstName = (reader["first_name"] != DBNull.Value) ? Convert.ToString(reader["first_name"]) : "";
                        account.LastName = (reader["last_name"] != DBNull.Value) ? Convert.ToString(reader["last_name"]) : "";
                        account.PhoneNumber = (reader["phone_number"] != DBNull.Value) ? Convert.ToString(reader["phone_number"]) : "";
                        if (reader["create_date"] != DBNull.Value)
                        {
                            account.CreateDate = (DateTime)reader["create_date"];
                        }

                       if (account != null)
                       {
                           new_account.AccountReport = account;
                           List<Order> list_orders = new List<Order>();
                           list_orders = AccountData.GetAccountOrders(account.PhoneNumber);
                           if (list_orders != null)
                           {
                               new_account.AccountOrders = list_orders;
                           }
                           list.Add(new_account);
                       }
                    }
                }
            }
            catch (Exception e)
            {
                string except = e.Message;
            }
            return list;
        }

        //Список заказов для аккаунта
        public static List<Order> GetAccountOrders(string phoneNumber)
        {
            List<Order> list = new List<Order>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetAccountOrders", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Order order = new Order();
                        order.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        order.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        order.OrderNumberService = (reader["order_number"] != DBNull.Value) ? Convert.ToString(reader["order_number"]) : "";
                        order.OrderNumberBank = (reader["order_system"] != DBNull.Value) ? Convert.ToString(reader["order_system"]) : "";
                        if (reader["cdate"] != DBNull.Value)
                        {
                            order.OrderDate = (DateTime)reader["cdate"];
                        }
                        order.Waiter = new Waiter { ID = (reader["waiterID"] != DBNull.Value) ? Convert.ToInt32(reader["waiterID"]) : 0, Name = (reader["waiterName"] != DBNull.Value) ? Convert.ToString(reader["waiterName"]) : "" };
                        if (order != null)
                        {
                            list.Add(order);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return list;
        }

        //Сброс пароля для аккаунта администратором
        public static string RedoPsw(int id, string phone)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.RedoPsw", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    //отправка SMS c временным паролем
                    SmsServiceSoapClient comm = new SmsServiceSoapClient();
                    string sessionID = comm.GetSessionID("novatorov", "VeeP2016");
                    Message msg = new Message();
                    msg.Data = DateTime.Now.ToString();
                    msg.SourceAddress = "VEEP";
                    msg.DestinationAddresses = new string[] { phone };
                    msg.Data = "Ваш временный пароль 3456";
                    msg.Validity = 10;
                    string[] result = comm.SendMessage(sessionID, msg);

                    return "Пароль успешно заменен";
                }
            }
            catch (Exception e)
            {
                return "Ошибка при смене пароля";
            }
        }
    }
}
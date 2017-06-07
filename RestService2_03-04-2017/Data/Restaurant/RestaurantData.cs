using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using RestService.Models;
using RestService.BLL;

namespace RestService
{
    public class RestaurantData
    {
        //Список сетей ресторанов, подключенных к сети
        public static List<RestNetwork> GetRestNetwork(string user_key, int language)
        {
            List<RestNetwork> list = new List<RestNetwork>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestNetwork", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RestNetwork restnetwork = new RestNetwork();
                        restnetwork.ID = (int)reader["id"];
                        restnetwork.Name = (language == 0) ? (string)reader["name"] : (string)reader["eng_name"];
                        restnetwork.Logo = (reader["logo"] != DBNull.Value) ? (string)reader["logo"] : "";
                        restnetwork.WWW = (reader["www"] != DBNull.Value) ? (string)reader["www"] : "";
                        restnetwork.Notes = (language == 0) ? ((reader["notes"] != DBNull.Value) ? (string)reader["notes"] : "") : ((reader["eng_notes"] != DBNull.Value) ? (string)reader["eng_notes"] : "");
                        restnetwork.Image = (reader["image"] != DBNull.Value) ? (string)reader["image"] : "";
                        //Список ресторанов, входящих в данную сеть
                        if (user_key == "admin")
                        {
                            restnetwork.Restaurants = GetRestaurantList(restnetwork.ID, language);
                        }
                        else
                        {
                            restnetwork.Restaurants = GetRestaurantListMobile(restnetwork.ID, language);
                        }
                        if (restnetwork != null)
                        {
                            list.Add(restnetwork);
                        }

                    }

                    //Список ресторанов, не входящих в сети
                    RestNetwork restnowork = new RestNetwork();
                    if (user_key == "admin")
                    {
                        restnowork.Restaurants = GetRestaurantList(0, language);
                    }
                    else
                    {
                        restnowork.Restaurants = GetRestaurantListMobile(0, language);
                    }
                    if (restnowork != null)
                    {
                        list.Add(restnowork);
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, user_key, "GetRestNetwork", "", "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Список ресторанов
        public static List<Restaurant> GetRestaurantList(int net_id, int language = 0)
        {
            List<Restaurant> list = new List<Restaurant>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestaurantList2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@net_id", SqlDbType.Int).Value = net_id;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Restaurant restaurant = new Restaurant();
                        restaurant.ID = (int)reader["id"];
                        restaurant.Name = (language == 0) ? (string)reader["name"] : (string)reader["eng_name"];
                        restaurant.Logo = (reader["logo"] != DBNull.Value) ? (string)reader["logo"] : "";
                        restaurant.Notes = (language == 0) ? ((reader["notes"] != DBNull.Value) ? (string)reader["notes"] : "") : ((reader["eng_notes"] != DBNull.Value) ? (string)reader["eng_notes"] : "");
                        restaurant.Image = (reader["image"] != DBNull.Value) ? (string)reader["image"] : "";
                        restaurant.WorkTime = (language == 0) ? (string)reader["work_time"] : (string)reader["eng_work_time"];
                        restaurant.Address = (language == 0) ? (string)reader["address"] : (string)reader["eng_address"];
                        restaurant.Phone = (reader["phone"] != DBNull.Value) ? (string)reader["phone"] : "";
                        restaurant.WWW = (reader["www"] != DBNull.Value) ? (string)reader["www"] : "";
                        restaurant.Geocode = (reader["geocode"] != DBNull.Value) ? (string)reader["geocode"] : "";
                        restaurant.Call = (reader["call"] != DBNull.Value) ? (bool)reader["call"] : false;
                        restaurant.Tipping = (reader["tipping"] != DBNull.Value) ? (int)reader["tipping"] : 1;
                        restaurant.TippingMin = (reader["tipping_min"] != DBNull.Value) ? (decimal)reader["tipping_min"] : 0;
                        restaurant.TippingMax = (reader["tipping_max"] != DBNull.Value) ? (decimal)reader["tipping_max"] : 0;
                        restaurant.IsPay = (reader["is_pay"] != DBNull.Value) ? (int)reader["is_pay"] : 0;
                        restaurant.Rating = GetRating(restaurant.ID);
                        if (restaurant != null)
                        {
                            //проверка доступности ресторана
                            try
                            {
                                string endpointName = "";
                                string address = "";
                                endpointName = Configs.GetEndpoint(restaurant.ID);
                                address = Configs.GetAddress(restaurant.ID);
                                /*
                                switch (restaurant.ID)
                                {
                                    case 202930001: /*Luce
                                        endpointName = "BasicHttpBinding_IIntegrationCMD";
                                        address = "http://92.38.32.63:9090/";
                                        break;
                                    case 209631111: /*Vogue
                                        endpointName = "BasicHttpBinding_IIntegrationCMD1";
                                        address = "http://185.26.193.5:9090/";
                                        break;
                                }
                                */
                                IntegrationCMD.IntegrationCMDClient module = new IntegrationCMD.IntegrationCMDClient(endpointName, address);
                                restaurant.IsActive = module.CheckRestaurant(restaurant.ID);
                            }
                            catch
                            {
                                restaurant.IsActive = 0;
                            }
                            list.Add(restaurant);
                        }
                    }

                    return list;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "GetRestaurantList", "", "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        public static List<Restaurant> GetRestaurantListMobile(int net_id, int language = 0)
        {
            List<Restaurant> list = new List<Restaurant>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestaurantList2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@net_id", SqlDbType.Int).Value = net_id;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Restaurant restaurant = new Restaurant();
                        restaurant.ID = (int)reader["id"];
                        restaurant.Name = (language == 0) ? (string)reader["name"] : (string)reader["eng_name"];
                        restaurant.Logo = (reader["logo"] != DBNull.Value) ? (string)reader["logo"] : "";
                        restaurant.Notes = (language == 0) ? ((reader["notes"] != DBNull.Value) ? (string)reader["notes"] : "") : ((reader["eng_notes"] != DBNull.Value) ? (string)reader["eng_notes"] : "");
                        restaurant.Image = (reader["image"] != DBNull.Value) ? (string)reader["image"] : "";
                        restaurant.WorkTime = (language == 0) ? (string)reader["work_time"] : (string)reader["eng_work_time"];
                        restaurant.Address = (language == 0) ? (string)reader["address"] : (string)reader["eng_address"];
                        restaurant.Phone = (reader["phone"] != DBNull.Value) ? (string)reader["phone"] : "";
                        restaurant.WWW = (reader["www"] != DBNull.Value) ? (string)reader["www"] : "";
                        restaurant.Geocode = (reader["geocode"] != DBNull.Value) ? (string)reader["geocode"] : "";
                        restaurant.Call = (reader["call"] != DBNull.Value) ? (bool)reader["call"] : false;
                        restaurant.Tipping = (reader["tipping"] != DBNull.Value) ? (int)reader["tipping"] : 1;
                        restaurant.TippingMin = (reader["tipping_min"] != DBNull.Value) ? (decimal)reader["tipping_min"] : 0;
                        restaurant.TippingMax = (reader["tipping_max"] != DBNull.Value) ? (decimal)reader["tipping_max"] : 0;
                        restaurant.IsPay = (reader["is_pay"] != DBNull.Value) ? (int)reader["is_pay"] : 0;
                        restaurant.Rating = GetRating(restaurant.ID);
                        if (restaurant != null)
                        {
                            list.Add(restaurant);
                        }
                    }

                    return list;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "GetRestaurantList", "", "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }
        //Информация о ресторане
        public static Restaurant RestaurantInfo(int restaurantID, string user_key, int language = 0)
        {
            Restaurant restaurant = new Restaurant();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestaurantInfo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        restaurant.ID = (int)reader["id"];
                        restaurant.Name = (language == 0) ? (string)reader["name"] : (string)reader["eng_name"];
                        restaurant.Logo = (reader["logo"] != DBNull.Value) ? (string)reader["logo"] : "";
                        restaurant.Notes = (language == 0) ? ((reader["notes"] != DBNull.Value) ? (string)reader["notes"] : "") : ((reader["eng_notes"] != DBNull.Value) ? (string)reader["eng_notes"] : "");
                        restaurant.Image = (reader["image"] != DBNull.Value) ? (string)reader["image"] : "";
                        restaurant.WorkTime = (language == 0) ? (string)reader["work_time"] : (string)reader["eng_work_time"];
                        restaurant.Address = (language == 0) ? (string)reader["address"] : (string)reader["eng_address"];
                        restaurant.Phone = (reader["phone"] != DBNull.Value) ? (string)reader["phone"] : "";
                        restaurant.WWW = (reader["www"] != DBNull.Value) ? (string)reader["www"] : "";
                        restaurant.Geocode = (reader["geocode"] != DBNull.Value) ? (string)reader["geocode"] : "";
                        restaurant.Call = (reader["call"] != DBNull.Value) ? (bool)reader["call"] : false;
                        restaurant.Tipping = (reader["tipping"] != DBNull.Value) ? (int)reader["tipping"] : 1;
                        restaurant.TippingMin = (reader["tipping_min"] != DBNull.Value) ? (decimal)reader["tipping_min"] : 0;
                        restaurant.TippingMax = (reader["tipping_max"] != DBNull.Value) ? (decimal)reader["tipping_max"] : 0;
                        restaurant.Rating = GetRating(restaurantID);
                    }
                    return restaurant;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "RestaurantInfo", "", "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Рейтинг ресторана
        protected static int GetRating(int restaurantID)
        {
            int ret = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRating", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantId", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    decimal i = 0;
                    decimal rating = 0;
                    while (reader.Read())
                    {
                        if (reader["rating"] != DBNull.Value)
                        {
                            rating = rating + Convert.ToDecimal((int)reader["rating"]);
                            i++;
                        }
                    }
                    ret = Convert.ToInt32(Math.Round(rating / i));
                    return ret;
                }
            }
            catch
            {
                return 0;
            }
        }

        //Поиск ID ресторана по его QR коду
        public static int GetRestaurantID(int qrID)
        {
            int ret = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestaurantOR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@qr_id", SqlDbType.Int).Value = qrID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["id"] != DBNull.Value)
                        {
                            ret = (int)reader["id"];
                        }
                    }
                    return ret;
                }
            }
            catch
            {
                return 0;
            }
        }

        //Скидка Veep для ресторана
        public static decimal GetVeepDiscount(int restaurantID)
        {
            int veep = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestaurantVeep", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["veep"] != DBNull.Value)
                        {
                            veep = (int)reader["veep"];
                        }
                    }
                    return veep;
                }
            }
            catch
            {
                return 0;
            }
        }

        //Список сетей ресторанов (админка)
        public static List<RestNetwork> GetRestNetworkList()
        {
            List<RestNetwork> list = new List<RestNetwork>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestNetwork", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RestNetwork restnetwork = new RestNetwork();
                        restnetwork.ID = (int)reader["id"];
                        restnetwork.Name = (string)reader["name"];
                        if (restnetwork != null)
                        {
                            list.Add(restnetwork);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "GetRestNetworkList", "", "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Список ресторанов (админка)
        public static List<Restaurant> GetRestaurants()
        {
            List<Restaurant> list = new List<Restaurant>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetRestaurants", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Restaurant restaurant = new Restaurant();
                        restaurant.ID = (int)reader["id"];
                        restaurant.Name = (string)reader["name"];
                        if (restaurant != null)
                        {
                            list.Add(restaurant);
                        }
                    }
                    return list;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "GetRestaurants", "", "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }
    }
}
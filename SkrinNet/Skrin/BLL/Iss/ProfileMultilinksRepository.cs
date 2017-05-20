using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skrin.BLL;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.Models.ProfileMultilinks;

namespace Skrin.BLL.Iss
{
    public class ProfileMultilinksRepository
    {
        protected ProfileMultilinksModel Prof = new ProfileMultilinksModel();

        public ProfileMultilinksRepository(string iss, int user_id)
        {
            Prof.iss = iss;
        }
        public async Task<ProfileMultilinksModel> GetProfileMultilinksAsync()
        {
            return Prof;
        }
    }
}
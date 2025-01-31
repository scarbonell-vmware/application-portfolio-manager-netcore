﻿using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_API.Controllers
{

    [Route("/api")]
    [ApiController]
    public class ContextController : ControllerBase
    {

        [HttpGet("envProps")]
        [Produces("application/json")]
        public async Task<IOrderedEnumerable<DictionaryEntry>> GetProps()
        {

            IOrderedEnumerable<DictionaryEntry> sortedEntries = Environment.GetEnvironmentVariables().Cast<DictionaryEntry>().OrderBy(entry => entry.Key);

            int maxKeyLen = sortedEntries.Max(entry => ((string)entry.Key).Length);

            string logMessage = "";

            foreach (var entry in sortedEntries)
            {
                logMessage =
                    logMessage +
                    Environment.NewLine +
                    (entry.Key + ": ").PadRight(maxKeyLen + 2) +
                    entry.Value;
            }

            //Console.WriteLine(logMessage);

            return  sortedEntries;
        }

        [HttpGet("version")]
        [Produces("application/json")]
        public async Task<Dictionary<string, string>> GetVersion()
        {

            var map = new Dictionary<string, string>
            {
                { "name", "1.0.0" }
            };

            return map;
        }

    }

    
    }

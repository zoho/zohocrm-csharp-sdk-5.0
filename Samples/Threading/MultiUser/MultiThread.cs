using System;
using System.Threading;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using SDKInitializer = Com.Zoho.Crm.API.Initializer;

namespace Com.Zoho.Crm.Sample.Threading.MultiUser
{
    public class MultiThread
    {
        DataCenter.Environment environment;
        IToken token;
        string moduleAPIName;

        public MultiThread(DataCenter.Environment environment, IToken token, string moduleAPIName)
        {
            this.environment = environment;
            this.token = token;
            this.moduleAPIName = moduleAPIName;
        }

        public static void RunMultiThreadWithMultiUser()
        {
            DataCenter.Environment env = USDataCenter.PRODUCTION;
            IToken token1 = new OAuthToken.Builder().ClientId("1000.xxxx").ClientSecret("xxx").RefreshToken("1000.xxxx.xxxx").RedirectURL("https://www.zoho.com").Build();
            DataCenter.Environment environment = USDataCenter.PRODUCTION;
            IToken token2 = new OAuthToken.Builder().ClientId("1000.xxxxx").ClientSecret("xxxxx").RefreshToken("1000.xxx.xxxx").RedirectURL("https://www.zoho.com").Build();
            new SDKInitializer.Builder().Environment(env).Token(token1).Initialize();
            MultiThread multiThread1 = new MultiThread(env, token1, "Vendors");
            Thread thread1 = new Thread(multiThread1.GetRecords);
            thread1.Start();
            MultiThread multiThread2 = new MultiThread(environment, token2, "Quotes");
            Thread thread2 = new Thread(multiThread2.GetRecords);
            thread2.Start();
            thread1.Join();
            thread2.Join();
        }

        public void GetRecords()
        {
            try
            {
                new SDKInitializer.Builder().Environment(this.environment).Token(this.token).SwitchUser();
                RecordOperations recordOperation = new RecordOperations();
                APIResponse<ResponseHandler> response = recordOperation.GetRecords(this.moduleAPIName, null, null);
                Console.WriteLine(JsonConvert.SerializeObject(response.Object));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
        }
    }
}

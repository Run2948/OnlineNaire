/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：ServiceManager
* 创 建 者：Qing
* 创建时间：2018-11-30 12:50:43
* CLR 版本：4.0.30319.42000
* 保存的文件名：ServiceManager
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    public class ServiceManager
    {
        /// <summary>
        /// 停止服务
        /// </summary>
        public static bool StopService(string ServiceName)
        {
            ServiceController service = new ServiceController(ServiceName);
            try
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        public static bool StartService(string ServiceName)
        {
            ServiceController service = new ServiceController(ServiceName);
            try
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        public static bool ReStartService(string ServiceName)
        {
            ServiceController service = new ServiceController(ServiceName);
            try
            {
                if (service.CanStop)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断服务是否已开启
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        public static bool ServiceIsRunning(string ServiceName)
        {
            ServiceController service = new ServiceController(ServiceName);
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

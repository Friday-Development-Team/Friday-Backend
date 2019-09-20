using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;

namespace Friday.Data.IServices {
    public interface IConfigurationService {
        Configuration GetConfig();
        /// <summary>
        /// Sets the Configuration options
        /// </summary>
        void SetConfig(Configuration con);

    }
}

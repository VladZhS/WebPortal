using System;
using System.Collections.Generic;

namespace WebPortalServer.Model.WebEnities
{
    public class ModelError : Dictionary<string, string>
    {
        public ModelError() { }
        public ModelError(string property, string error)
        {
            this.AddError(property, error);
        }
        public void AddError(string property, string error)
        {
            if (this.ContainsKey(property))
                error = this[property] + Environment.NewLine + error;

            this[property] = error;
        }
    }
}

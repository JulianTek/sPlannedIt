using System;
using sPlannedIt.Data;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Factories
{
    public static class CompanyFactory
    {
        private static ICompanyHandler _companyHandler;

        public static ICompanyHandler CompanyHandler
        {
            get
            {
                if (_companyHandler == null)
                {
                    _companyHandler = new CompanyHandler();
                }

                return _companyHandler;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess
{
    [Description("Test buisness process - " +
        "responsible for obtaining an online loan by the customer company")]
    public class BankLoanTestProcess : BaseProcess
    {
        #region Process state-property

        public string SelectedProduct { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }

        public string CompanyIdentificator { get; set; }

        public CompanyData CompanyData { get; set; }

        public LegalForm LegalForm { get; set; }
        public List<PersonData> CompanyMembers { get; set; } = new List<PersonData>();

        #endregion

        /// <summary>
        /// Start procesu
        /// </summary>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        protected override object Start(object inputParameter)
        {
            SetStepData("Start proces");

            Log("Step 1 - show registratio");
            Registration();

            Log("Step 2 - email validation");
            EmailVerification();

            Log("Step 3 - sms validation");
            SmsVerification();

            Log("Step 4 - get company Id");
            GetCompanyId();

            Log("Step 5 - get company data");
            GetCompanyData();

            Log("Step 6 - get company legal form");
            GetCompanyLegalForm();

            Log("Step 7 - chceck company legalForm");
            if(LegalForm == LegalForm.Corporation)
            {
                Log("Step 8 - termainate - corporation not suportet");
                CorporationPath();
            } 
            else if( LegalForm == LegalForm.LimitedLiabilityCompany)
            {
                Log("Step 9 - create subprocess for accept from all members");
                LLCPath();
            } 
            else if(LegalForm == LegalForm.SolePropietorship)
            {
                Log("Step 10 - accept");
                SolePropietorshipPath();
            }

            Log("Make an application");
            MakeApplication();

            Log("wysyłam sms z weyfikacją");
            var code = generateAndSendValidationSms(ClientPhoneNumber);
            var smsValidationResponse = ShowForm("SmsVeryfication", code);
            validateSmsVeryfication(smsValidationResponse);

            
            return code.ValidationCode;
        }               

        #region Registration
        [Interpret]
        protected void Registration()
        {
            SetStepData("Registration", "Get email, phone number and agreements from Custromer");
            var registration = getRegistrationStepModel();
            var registrationResponse = ShowForm("registration", registration);

            SetStepData("Registration-response", "Custromer put contact data");
            Log("check, just in case, whether approved consents");
            validateRegistrationStepResponse(registrationResponse);

            SetStepData("Registration-approve", "Custromer approve consents");

            Log("Save the Customer selections");
            SelectedProduct = registrationResponse.SelectedProduct;
            ClientEmail = registrationResponse.Email;
            ClientPhoneNumber = registrationResponse.PhoneNumber;
        }

        private RegistrationStepData getRegistrationStepModel()
        {
            var step = new RegistrationStepData();
            step.ProducstsList.Add("Service product"); //example
            step.ProducstsList.Add("Box product"); //example
            step.Agreements.Add(new StepAgreement() { Content = "Consent to phone contact" });
            step.Agreements.Add(new StepAgreement() { Content = "Consent to email contact" });
            return step;
        }

        private void validateRegistrationStepResponse(RegistrationStepData firstStepResponse)
        {
            if (firstStepResponse.Agreements.All(x => x.Accepted) == false)
            {
                EndProcess("No approvals in consents", firstStepResponse);
            }
        }

        #endregion
        #region Email verification

        [Interpret]
        protected void EmailVerification()
        {
            var emailModel = generateAndSendValidationEmail(ClientEmail);
            var response = ShowForm("EmailValidation", emailModel);
            if (response.ValidationCodeFromUser != response.ValidationCode)
            {
                EndProcess("Błędny kod weryfikacyjny", "Podano błędny kod weryfikacyjny");
            }
        }

        private EmailValidationData generateAndSendValidationEmail(string clientEmail)
        {
            var code = new EmailValidationData();
            code.Email = clientEmail;
            code.ValidationCode = "1234";
            //wysłam przy pomocy jakiś serwisów sms
            //externalService.SendEmail(code);

            return code;
        }

        #endregion
        #region Sms verification

        [Interpret]
        protected void SmsVerification()
        {
            var smsModel = generateAndSendValidationSms(ClientEmail);
            var response = ShowForm("SmsValidation", smsModel);
            validateSmsVeryfication(response);
        }

        private void validateSmsVeryfication(SmsValidationData smsValidationResponse)
        {
            if (smsValidationResponse.ValidationCodeFromUser != smsValidationResponse.ValidationCode)
            {
                EndProcess("Błędny kod weryfikacyjny", "Podano błędny kod weryfikacyjny");
            }
        }

        private SmsValidationData generateAndSendValidationSms(string clientPhoneNumber)
        {
            var code = new SmsValidationData();
            code.PhoneNumber = clientPhoneNumber;
            code.ValidationCode = "1234";
            //wysłam przy pomocy jakiś serwisów sms

            return code;
        }


        #endregion
        #region Company Id

        [Interpret]
        protected void GetCompanyId()
        {
            var companyData = new CompanyData();
            var response = ShowForm("CompanyIdentificator", companyData);
            CompanyIdentificator = response.CompanyIdentificator;
        }

        #endregion
        #region Company data

        [Interpret]
        protected void GetCompanyData()
        {
            Log("Get customer company data from external databases");
            var companyData = getCompanyDataFromExternalDataBase(CompanyIdentificator);

            Log("Show company data to Customer to validate");
            var response = ShowForm("CompanyData", companyData);

            CompanyData = response;
        }

        private CompanyData getCompanyDataFromExternalDataBase(string companyIdentificator)
        {
            var companyData = new CompanyData();
            companyData.CompanyIdentificator = companyIdentificator;
            //example: get data from external database
            companyData.CompanyName = "Example Company name";
            companyData.CompanyAddress = "Example addres";
            companyData.CompanyCity = "Example City";

            return companyData;
        }

        #endregion
        #region Get company legal form

        [Interpret]
        protected void GetCompanyLegalForm()
        {
            // in real world - it be returnet from external database
            LegalForm = LegalForm.LimitedLiabilityCompany;
            CompanyMembers.Add(new PersonData() { Name = "John Smith", PersonIdentificator = "1" });
            CompanyMembers.Add(new PersonData() { Name = "Joe Public", PersonIdentificator = "2" });
        }

        #endregion
        #region Corporation path
        [Interpret]
        protected void CorporationPath()
        {
            SetStepData("LegalForm-Corporation", "The process does not support corporations");
            EndProcess("Not supported", "The process does not support corporations");
        }

        #endregion
        #region LLC path

        [Interpret]
        protected void LLCPath()
        {
            Log("Subprocess for member of managment board");
            SetStepData("LLC-subProcessForMembers", "Start subprocess for all company members");
            foreach (var memberPerson in this.CompanyMembers)
            {
                CreateNewChildProcess<CompanyMeberVerificationAndConsentProcess>(memberPerson);
            }

            SetStepData("LLC-waitingForAllConsent",
                "The main process waits for child processes to complete successfully.");

            WaitToEndAllChildProcesses();
        }

        #endregion
        #region SolePropietorship path

        protected void SolePropietorshipPath()
        {
            var applicationAcceptData = new ApplicationAcceptData();
            var response = ShowForm("SolePropietorshipApplicationAccept", applicationAcceptData);

        }

        #endregion
        #region Make an application
        [Interpret]
        protected void MakeApplication()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Publiczne metody procesu

        public string GetClientPhoneNumber()
        {
            return ClientPhoneNumber;
        }

        #endregion
    }


    public enum LegalForm
    {
        Corporation,
        LimitedLiabilityCompany,
        SolePropietorship
    }
}

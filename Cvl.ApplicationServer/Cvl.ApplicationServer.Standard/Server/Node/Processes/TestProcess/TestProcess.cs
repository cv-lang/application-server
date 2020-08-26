using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Cvl.ApplicationServer.Server.Node.Processes.TestProcess.Steps;

namespace Cvl.ApplicationServer.Server.Node.Processes.TestProcess
{
    public class TestProcess : BaseProcess
    {
        #region Stan procesu

        public string SelectedProduct { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }

        #endregion

        /// <summary>
        /// Start procesu
        /// </summary>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        protected override object Start(object inputParameter)
        {

            Log("wyświetlam pierwszą formatkę procesu");
            var firstStep = getFirstStepModel();
            var firstStepResponse = ShowForm("startForm", firstStep);

            Log("sprawdzam na wszelki wypadek czy zatwierdzone zgody");
            validateFirstStepResponse(firstStepResponse);


            Log("zapisuje zaznaczenie klienta");
            SelectedProduct = firstStepResponse.SelectedProduct;
            ClientEmail = firstStepResponse.Email;
            ClientPhoneNumber = firstStepResponse.PhoneNumber;

            Log("wysyłam sms z weyfikacją");
            var code = generateAndSendValidationSms(ClientPhoneNumber);
            var smsValidationResponse = ShowForm("SmsVeryfication", code);
            validateSmsVeryfication(smsValidationResponse);

            
            return code.ValidationCode;
        }

        private void validateSmsVeryfication(SmsValidationData smsValidationResponse)
        {
            if (smsValidationResponse.ValidationCodeFromUser != smsValidationResponse.ValidationCode)
            {
                EndProcess("Błędny kod weryfikacyjny", "Podano błędny kod weryfikacyjny");
            }
        }

        private void validateFirstStepResponse(FirstStepData firstStepResponse)
        {
            if (firstStepResponse.Agreements.All(x => x.Accepted) == false)
            {
                EndProcess("Brak zatwierdzeń w Agrements", firstStepResponse);
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

        private FirstStepData getFirstStepModel()
        {
            var step = new FirstStepData();
            step.ProducstsList.Add("Service product");
            step.ProducstsList.Add("Box product");
            step.Agreements.Add(new StepAgreement(){Content = "Zgoda na kontakt telefoniczny"});
            step.Agreements.Add(new StepAgreement() { Content = "Zgoda na kontakt mailowy" });
            return step;
        }

        #region Publiczne metody procesu

        public string GetClientPhoneNumber()
        {
            return ClientPhoneNumber;
        }

        #endregion
    }
}

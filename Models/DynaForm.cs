using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using FormBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace FormBot.Models
{
    [Serializable]
    public class DynaForm
    {
        public Customer Source { get; set; }

        public Customer Destination { get; set; }

        public static IForm<DynaForm> BuildFormAsync()
        {
            List<Customer> allCustomers = new List<Customer>()
            {
                new Customer("Harry"),
                new Customer("Ron"),
                new Customer("Hermione")
            };

            try
            {

            return new FormBuilder<DynaForm>()
                .Message("Welcome to the simple sandwich order bot!")
                .Field(new FieldReflector<DynaForm>(nameof(Source))
                    .SetType(null)
                    .SetFieldDescription(nameof(Source))
                    .SetFieldTerms(nameof(Source))
                    .SetDefine((state, field) => {

                        foreach (var customer in allCustomers)
                        {
                            field
                                .AddDescription(customer, new DescribeAttribute(description: customer.Name))
                                .AddTerms(customer, customer.Name);
                        }

                        return Task.FromResult(true);
                    })
                    .SetPrompt(new PromptAttribute("Select the source account \n {||} \n")
                    {
                        ChoiceStyle = ChoiceStyleOptions.Buttons
                    })
                    .SetAllowsMultiple(false)
                )

                .Field(new FieldReflector<DynaForm>(nameof(Destination))
                    .SetType(null)
                    .SetFieldDescription(nameof(Destination))
                    .SetDependencies(nameof(Destination))
                    .SetFieldTerms(nameof(Destination))
                    .SetDefine((state, field) =>
                    {

                        foreach (var customer in allCustomers)
                        {
                            field
                                .AddDescription(customer, new DescribeAttribute(description: customer.Name))
                                .AddTerms(customer, customer.Name);
                        }

                        return Task.FromResult(true);
                    })
                    .SetPrompt(new PromptAttribute("Select the destination account \n {||} \n")
                    {
                        ChoiceStyle = ChoiceStyleOptions.Buttons
                    })
                    .SetAllowsMultiple(false)
                )

                .Confirm("Do you want to continue? {||} ")
                .OnCompletion(async (context, order) =>
                {
                    await context.PostAsync("Excellent! Your order has been placed. :)");
                })
                .Build();
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }
        }
    }
}
delete from AccountAttachments Where AccountExpenseEntryId in (Select AccountExpenseEntryId from AccountExpenseEntry Where AccountId <> 1631)
delete from AccountExpenseEntry where accountid <> 1631
delete from AccountExpense where accountid <> 1631
delete From AccountExpenseTypeTaxCode where accountid <> 1631
delete from AccountExpenseType where accountid <> 1631
delete from AccountTaxCode where accountid <> 1631
delete from AccountPaymentMethod where accountid <> 1631
delete from AccountPreferences where accountid <> 1631
delete from AccountEmployeeTimeEntry Where AccountProjectId in (Select AccountProjectId from AccountProject Where AccountId <> 1631)
delete from AccountEmployeeTimeEntry Where AccountProjectTaskId in (Select AccountProjectTaskId from AccountProjectTask Where AccountProjectId  in (Select AccountProjectId from AccountProject Where AccountId <> 1631))
Delete From AccountProjectTask Where AccountProjectId in (Select AccountProjectId from AccountProject Where AccountId <> 1631)
delete from AccountProjectMilestone Where AccountProjectId in (Select AccountProjectId from AccountProject Where AccountId <> 1631)
delete from accountemailnotificationpreference where accountprojectid not in (Select AccountProjectId from AccountProject where accountid = 1631)
delete from accountemailnotificationpreference where AccountEmployeeId not in (Select AccountEmployeeId from AccountEmployee where accountid = 1631)
delete from accountemailnotificationpreference where AccountId <> 1631
delete from AccountProject where accountid <> 1631
delete from accountstatus where accountid <> 1631
Delete From EmailMessage where EmailTo in (Select EmailAddress from AccountEmployee Where AccountId <> 1631)
delete from accountemployeeattendance where accountid <> 1631
delete from accountemployee where accountid  <> 1631
delete from Account where accountid <> 1631
Delete From AccountPriority where accountid <> 1631
delete from AccountWorkTypeBillingRate where accountid <> 1631
delete from AccountCurrencyExchangeRate where accountid <> 1631
delete From AccountBillingRate where accountid <> 1631
delete from AccountCurrency where accountid <> 1631
delete From AccountTaxZone where accountid <> 1631
delete From AccountCostCenter where accountid <> 1631
delete From AccountWorkType where accountid <> 1631
Delete From LTCustomer 
Delete From LTCustomerPayment
Delete from EmailMessage
Delete From Audit
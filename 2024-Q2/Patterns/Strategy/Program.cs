using Strategy;

var creditCardPayment = new CreditCardPayment();
var payPalPayment = new PayPalPayment();
var cryptoPayment = new CryptoPayment();

var creditCardPaymentService = new PaymentService(creditCardPayment);
creditCardPaymentService.Pay(100.00);

var payPalPaymentService = new PaymentService(payPalPayment);
payPalPaymentService.Pay(65.95);

var cryptoPaymentService = new PaymentService(cryptoPayment);
cryptoPaymentService.Pay(245.99);

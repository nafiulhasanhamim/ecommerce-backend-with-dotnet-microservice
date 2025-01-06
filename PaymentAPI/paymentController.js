const axios = require("axios");
const paymentModel = require("./paymentModal");
const globals = require("node-global-storage");
const { v4: uuidv4 } = require("uuid");
class paymentController {
  bkash_headers = async () => {
    return {
      "Content-Type": "application/json",
      Accept: "application/json",
      authorization: globals.get("id_token"),
      "x-app-key": process.env.bkash_api_key,
    };
  };

  payment_create = async (req, res) => {
    const { amount, userId, orderId } = req.body;
    globals.set("userId", userId);
    try {
      const { data } = await axios.post(
        process.env.bkash_create_payment_url,
        {
          mode: "0011",
          payerReference: orderId,
          callbackURL: "http://localhost:5000/api/bkash/payment/callback",
          amount: amount,
          currency: "BDT",
          intent: "sale",
          merchantInvoiceNumber: "Inv" + uuidv4().substring(0, 5),
        },
        {
          headers: await this.bkash_headers(),
        }
      );
      return res.status(200).json({ bkashURL: data.bkashURL });
    } catch (error) {
      return res.redirect(
        `http://localhost:4200/error?message=${error.message}`
      );
    }
  };

  call_back = async (req, res) => {
    const { paymentID, status } = req.query;
    var Data = "";
    if (status === "cancel" || status === "failure") {
      return res.redirect(`http://localhost:4200/error?message=${status}`);
    }
    if (status === "success") {
      try {
        const { data } = await axios.post(
          process.env.bkash_execute_payment_url,
          { paymentID },
          {
            headers: await this.bkash_headers(),
          }
        );
        const orderId = data.payerReference;
        let url = "http://localhost:5008/api/Order/payment";
        const eventType = "payment_success";
        const response = await axios.post(
          url,
          { orderId, eventType },
          {
            headers: {
              "Content-Type": "application/json",
            },
          }
        );
        Data = data;
        if (data && data.statusCode === "0000") {
          return res.redirect(`http://localhost:4200/orders`);
        }
      } catch (error) {
        if (Data.statusMessage == "Successful")
          return res.redirect(`http://localhost:4200/orders`);
        else return res.redirect(`http://localhost:4200/orders`);
      }
    }
  };

  refund = async (req, res) => {
    const { trxID } = req.params;
    try {
      const payment = await paymentModel.findOne({ trxID });
      const { data } = await axios.post(
        process.env.bkash_refund_transaction_url,
        {
          paymentID: payment.paymentID,
          amount: payment.amount,
          trxID,
          sku: "payment",
          reason: "cashback",
        },
        {
          headers: await this.bkash_headers(),
        }
      );
      if (data && data.statusCode === "0000") {
        return res.status(200).json({ message: "refund success" });
      } else {
        return res.status(404).json({ error: "refund failed" });
      }
    } catch (error) {
      return res.status(404).json({ error: "refund failed" });
    }
  };
}

module.exports = new paymentController();

// Decompiled with JetBrains decompiler
// Type: FeedbackManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Net;
using System.Net.Mail;
using System.Net.Security;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackManager : MonoBehaviour
{
  public InputField feedback;
  public InputField username;
  public InputField email;
  public GameObject feebackForm;
  public GameObject feedbackSuccess;
  public GUI_PauseWindow pauseWindow;
  public string feedbackSendToEmail;

  public void SendMail()
  {
    MailMessage message = new MailMessage();
    message.From = new MailAddress("axisgames@gmail.com");
    message.To.Add(this.feedbackSendToEmail);
    message.Subject = "Feedback from beta tester about Axis Football";
    message.Body = "User: " + this.username.text + "\nEmail: " + this.email.text + "\n\nGave the following feedback:\n" + this.feedback.text;
    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
    smtpClient.Port = 587;
    smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential("AxisFootballFeedback@gmail.com", "Retlow_85");
    smtpClient.EnableSsl = true;
    ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((s, certificate, chain, sslPolicyErrors) => true);
    if (this.feedback.text.Length > 0)
      smtpClient.Send(message);
    Debug.Log((object) "Successfully Sent Email");
    this.CloseFeedback();
    this.pauseWindow.HideWindow();
  }

  public void CloseFeedback()
  {
    this.feebackForm.SetActive(false);
    this.feedbackSuccess.SetActive(true);
  }

  public void CloseFeedbackSuccess() => this.feedbackSuccess.SetActive(false);
}

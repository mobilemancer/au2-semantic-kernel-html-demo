import { HttpClient } from "@aurelia/fetch-client";

export class UsingPlugins{
    public draft: string;
    public response: string;
  
    private httpClient: HttpClient;
    private endPoint = "usingplugins";
  
    constructor() {
      this.httpClient = new HttpClient();
  
      this.httpClient.configure((config) => {
        config
          .withDefaults({ mode: "cors" })
          .withBaseUrl("http://localhost:7123/api/");
      });
    }
  
    public async sendText(): Promise<void> {
      console.log("Sending text...");
  
      this.httpClient
        .post(this.endPoint, this.draft)
        .then((response) => response.text())
        .then((text) => (this.response = text))
        .catch((error) => console.error(error));
  
      console.log("Text sent successfully!");
    }
}
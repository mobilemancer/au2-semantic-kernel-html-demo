import { HttpClient } from '@aurelia/fetch-client';

export class MyApp {

  public draft: string;
  private httpClient: HttpClient;
  private endPoint = "drafts";

  /**
   *
   */
  constructor() {
    this.httpClient = new HttpClient();

    this.httpClient.configure(config => {
      config
        .withDefaults({ mode: 'cors' })
        .withBaseUrl('http://localhost:7123/api/');
    });
  }

  public async sendText(): Promise<void> {
    console.log('Sending text...');
    console.log(this.draft);

    this.httpClient.post(this.endPoint, this.draft)
      .then(response => response.json())
      .then(users => console.log(users))
      .catch(error => console.error(error));

    console.log('Text sent successfully!');
  }
}

import { Method, default as axios } from "axios"

export default class apiClient {
  baseUrl: string;

  constructor(baseUrl: string){
    this.baseUrl = baseUrl;
  }

  public async get(address: string, queryParams: Array<[string, string]>) {

    //implements AxiosRequestConfig
    let config = {
      method: <Method>"GET",
      params: queryParams,
      url: this.baseUrl + address
    };

    let response = await axios(config)
    let rawData = response.data;

    return rawData;
  }
}


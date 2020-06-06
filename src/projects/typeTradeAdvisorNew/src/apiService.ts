import { Method, default as axios } from "axios"

export default class apiService {
  public static async get(baseURL: any, queryParams: any, dataParser: any, dataProcessor: any) {
    let reqURI = baseURL;

    //implements AxiosRequestConfig
    let config = {
      method: <Method>"GET",
      params: queryParams,
      url: baseURL
    };

    let response = await axios(config)
    let rawData = response.data;

    var parsedData = dataParser(rawData);

    return parsedData;
  }
}


import {stringify} from "query-string";
const fetch = require("node-fetch");

export interface ICodalDataGrabber {
    getPage(pageNo: number): any;
}

export class CodaDataGrabber implements ICodalDataGrabber{

    async getPage(pageNumber: number): Promise<any> {
        let codalPageJsonData;

        const queryString = stringify({
            PageNumber: pageNumber,
        });

        await fetch('https://search.codal.ir/api/search/v2/q?' + queryString)
            .then(res =>{
                codalPageJsonData = res.json();
            });
        return  codalPageJsonData;
    }
}


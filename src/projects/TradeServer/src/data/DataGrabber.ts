const fetch = require('node-fetch');



export interface ICodalDataGrabber {
    getPage(pageNo: number): any;

}

export class CodaDataGrabber implements ICodalDataGrabber{

    async getPage(pageNo: number): Promise<any> {
        let codalPageJsonData;
        await fetch('https://search.codal.ir/api/search/v2/q?&PageNumber=1')
            .then(res =>{
                codalPageJsonData = res.json();
            });

        return  codalPageJsonData;
    }
}


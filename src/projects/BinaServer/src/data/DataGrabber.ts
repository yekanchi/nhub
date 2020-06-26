import {stringify} from "query-string";
import {Letter, Sheet} from "../domain/Domain";
import { JSDOM } from "jsdom"
import {url} from "inspector";
const fetch = require("node-fetch");


export interface ICodalDataGrabber {
    getPage(pageNo: number): any;
    getSheets(letter: Letter): any;
}

export class CodaDataGrabber implements ICodalDataGrabber{
    baseUrl: string = "http://codal.ir";
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

    async getSheets(letter: Letter): Promise<Letter>{
        let html: any;
        let url = this.baseUrl + letter.url;
        url = 'https://codal.ir/Reports/Decision.aspx?LetterSerial=da6b5G%2BmyGdATjYMTGWjIw%3D%3D&rt=2&let=8&ct=0&ft=-1';
        await fetch(url)
            .then(res =>
                res.text())
            .then(body => {
                // console.log(body);
                html = body;
            });

        const dom = new JSDOM(html);

        let selectedSheetOption = dom.window.document.querySelector("[id*='ddlTable'] option[selected]");
        let sheetOptions = dom.window.document.querySelectorAll("[id*='ddlTable'] option:not([selected])");

        let firstSheet: Sheet;
        firstSheet = new Sheet();
        if(selectedSheetOption != null){
            firstSheet.sheetId= +selectedSheetOption.getAttribute("value");
            firstSheet.title = selectedSheetOption.innerHTML;

            const queryString = stringify({
                sheetId: firstSheet.sheetId,
            });

            await fetch(url + '&' +queryString)
                .catch(err=>
                console.log(err))
                .then(res =>
                    res.text())
                .then(body => {
                    // console.log(body);
                    firstSheet.html = body;
                });
            letter.sheets.push(firstSheet);
        }

        for (const option of sheetOptions){
            let newSheet: Sheet;
            newSheet = new Sheet();
            newSheet.sheetId= +option.getAttribute("value");
            newSheet.title = option.innerHTML;

            const queryString = stringify({
                sheetId: firstSheet.sheetId,
            });

            await fetch(url + '&' +queryString)
                .catch(err=>
                    console.log(err))
                .then(res =>
                    res.text())
                .then(body => {
                    // console.log(body);
                    newSheet.html = body;
                });
            letter.sheets.push(newSheet);
        }

        return letter;
    }




}


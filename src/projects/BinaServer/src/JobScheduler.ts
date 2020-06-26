import {CodaDataGrabber , ICodalDataGrabber} from "./data/DataGrabber"
import {Letter} from "./domain/Domain";
import {DataAccess, IDataAccess} from "./dal/DataAccess";


export class JobScheduler {
    private _dataGrabber: ICodalDataGrabber
    private _dataAccess: IDataAccess;

    constructor() {
        this._dataGrabber = new CodaDataGrabber();
        this._dataAccess = new DataAccess();
    }

    async persist(letters: Array<Letter>): Promise<any> {
        for (const letter of letters){
            let resultLetter = await this._dataAccess.insert(letter);
        }
        return true;
    }

    public async RunGrabber(): Promise<void>{
        let populatedLetters = new Array<Letter>();

        for (let pageNo = 1; pageNo < 10; pageNo++){
            try {
                const codalPage = await this._dataGrabber.getPage(pageNo);
                for (const letter of codalPage.Letters) {
                    let newLetter = new Letter({
                        tracingNo: letter.TracingNo,
                        symbol: letter.Symbol,
                        companyName: letter.CompanyName,
                        underSupervision: letter.UnderSupervision,
                        title: letter.Title,
                        letterCode: letter.LetterCode,
                        sentDateTime: letter.SentDateTime,
                        publishDateTime: letter.PublishDateTime,
                        hasHtml: letter.HasHtml,
                        url: letter.Url,
                        hasExcel: letter.HasExcel,
                        hasPdf: letter.HasPdf,
                        hasXbrl: letter.HasXbrl,
                        hasAttachment: letter.HasAttachment,
                        attachmentUrl: letter.AttachmentUrl,
                        pdfUrl: letter.PdfUrl,
                        excelUrl: letter.ExcelUrl,
                        xbrlUrl: letter.XbrlUrl,
                        tedanUrl: letter.TedanUrl
                    });
                    newLetter = await this._dataGrabber.getSheets(newLetter);
                    populatedLetters.push(newLetter);
                }
                await this.persist(populatedLetters);
            } catch (exception) {
                console.table(exception);
            }

        }


    }
}
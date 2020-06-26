import {createConnection, Repository} from "typeorm";
import {Letter, Sheet, User} from "../../../domain/Domain";

export const getBinaCodalDbConnection = async () => {
    return await createConnection({
        type: "sqlite",
        database: "../../databases/BinaCodal.db",
        logging: false,
        synchronize: true,
        entities: [User]
    });
}
//todo: m.talebi: use relative database path
export var getDumpCodalDbConnection = async () => {
    return await createConnection({
        type: "sqlite",
        database: "E:\\NHUB\\src\\projects\\BinaServer\\databases\\dumpCodal.db",
        logging: false,
        entities: [Letter, Sheet]
    });
}

export class DumpRepository implements IDumpRepository {
    repository: Repository<Letter>

    constructor() {
    }

    async insert(letter: Letter): Promise<Letter> {
        let connection = await getDumpCodalDbConnection();
        let repo = connection.getRepository(Letter);
        let result = await repo.save<Letter>(letter);

        await connection.close();
        return result;
    }

    async createArray(letters: Array<Letter>): Promise<any> {
        let generatedResponse = [];
        for (let letter of letters) {
            try {
                let insertResponse = await this.insert(letter)
                generatedResponse.push(insertResponse)
            } catch (error) {
                //todo: m.talebi: log using global logger;
                console.log('insert error: ' + error);
            }
        }
        return generatedResponse;
    }

    async getAll(): Promise<Letter[]> {
        return await this.repository.find();
    }
}

export class SheetRepository implements ISheetRepository {
    repository: Repository<Sheet>

    constructor() {
    }

    async insert(sheet: Sheet): Promise<Sheet> {
        let connection = await getDumpCodalDbConnection();
        let repo = connection.getRepository(Sheet);
        let result = await repo.save<Sheet>(sheet);
        await connection.close();

        return result;
    }

    async createArray(sheets: Array<Sheet>): Promise<any> {
        let generatedResponse = [];
        for (let sheet of sheets) {
            try {
                let insertResponse = await this.insert(sheet)
                generatedResponse.push(insertResponse)
            } catch (error) {
                //todo: m.talebi: log using global logger;
                console.log('insert error: ' + error);
            }
        }
        return generatedResponse;
    }

    async getAll(): Promise<Sheet[]> {
        return await this.repository.find();
    }
}

export interface ISheetRepository {
    insert(sheet: Sheet): Promise<Sheet>;
}

export interface IDumpRepository {

    insert(letter: Letter): Promise<Letter>;

    getAll(): Promise<Letter[]>;
}
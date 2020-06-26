

import {Connection, Repository} from "typeorm";
import {Letter, User} from "../domain/Domain";
import {
    DumpRepository,
    IDumpRepository,
    ISheetRepository,
    SheetRepository
} from "./repositories/sqlite/dumpCodaRepositories";

export class DataAccess implements IDataAccess {
    private _dumpRepository: IDumpRepository;
    private _sheetRepository: ISheetRepository;

    constructor() {
        this._dumpRepository = new DumpRepository();
        this._sheetRepository = new SheetRepository();
    }

    async insert(letter: Letter) {
        let persistedLetter = await this._dumpRepository.insert(letter);
        letter.sheets.forEach(s => s.letterId = persistedLetter.id);
        for (const sheet of letter.sheets)
        {
            await this._sheetRepository.insert(sheet);
        }

    }
}

export interface IDataAccess {
        insert(letter: Letter): Promise<void>;
}



export class UserRepository {
    repository: Repository<User>

    constructor(connection: Connection) {
        this.repository = connection.getRepository(User);
    }

    async create(user: User): Promise<User> {
        return this.repository.save(user);
    }

    async getAll(): Promise<User[]> {
        return await this.repository.find();
    }
}


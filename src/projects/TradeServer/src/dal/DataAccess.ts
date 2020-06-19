import {Connection, createConnection, Repository} from "typeorm";
import {Letter, User} from "../domain/Domain";
import {IDumpRepository} from "./DataAccessContract";

export const getBinaCodalDbConnection = async () => {
    return await createConnection({
        type: "sqlite",
        database: "../../databases/BinaCodal.db",
        logging: false,
        synchronize: true,
        entities: [User]
    });
}

export const getDumpCodalDbConnection = async () => {
    return await createConnection({
        type: "sqlite",
        database: "E:\\NHUB\\src\\projects\\TradeServer\\databases\\dumpCodal.db",
        logging: false,
        entities: [Letter]
    });
}

export class DumpRepository implements IDumpRepository {
    repository: Repository<Letter>

    constructor() {
    }

    async insert(letter: Letter): Promise<Letter> {
        let connection = await getDumpCodalDbConnection();
        this.repository = connection.getRepository(Letter);
        let result = await this.repository.save<Letter>(letter);
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


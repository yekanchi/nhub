import { Connection, Repository } from "typeorm";
import { Letter, User } from "../domain/Domain";
import { IDumpRepository } from "./DataAccessContract";
export declare const getBinaCodalDbConnection: () => Promise<Connection>;
export declare const getDumpCodalDbConnection: () => Promise<Connection>;
export declare class DumpRepository implements IDumpRepository {
    repository: Repository<Letter>;
    constructor();
    insert(letter: Letter): Promise<Letter>;
    createArray(letters: Array<Letter>): Promise<any>;
    getAll(): Promise<Letter[]>;
}
export declare class UserRepository {
    repository: Repository<User>;
    constructor(connection: Connection);
    create(user: User): Promise<User>;
    getAll(): Promise<User[]>;
}
//# sourceMappingURL=DataAccess.d.ts.map
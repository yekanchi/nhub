import { Connection, Repository } from "typeorm";
import { User } from "./domain/Domain";
export declare function RunService(): Promise<void>;
export declare class UserRepository {
    repository: Repository<User>;
    constructor(connection: Connection);
    create(user: User): Promise<User>;
    getAll(): Promise<User[]>;
}
//# sourceMappingURL=main.d.ts.map
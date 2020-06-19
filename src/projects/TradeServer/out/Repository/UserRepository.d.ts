import { Letter, User } from "../domain/Domain";
import { Repository } from "typeorm";
export declare class UserRepository extends Repository<User> {
    get(id: number): Promise<User>;
}
export declare class DumpDataRepository extends Repository<Letter> {
    get(id: number): Promise<Letter>;
}
//# sourceMappingURL=UserRepository.d.ts.map
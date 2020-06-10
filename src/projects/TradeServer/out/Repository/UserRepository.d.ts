import { User } from "../domain/Domain";
import { Repository } from "typeorm";
export declare class UserRepository extends Repository<User> {
    get(id: number): Promise<User>;
}
//# sourceMappingURL=UserRepository.d.ts.map
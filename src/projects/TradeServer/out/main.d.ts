import { User } from "./domain/Domain";
export declare function RunService(): Promise<void>;
export declare class UserRepository {
    entityManager: any;
    constructor();
    create(user: User): Promise<void>;
}
//# sourceMappingURL=main.d.ts.map
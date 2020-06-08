import {createDbConnection} from "./Service/DataService";
import {EntityManager, getManager} from "typeorm";
import {User} from "./domain/Domain";

export async function RunService() {

    let someUser = new User({userName: "m.talebi", name: "morteza", family: "talebi"});
    let repo = new UserRepository();

    await repo.create(someUser);
}


export class UserRepository {
    entityManager: any;
    constructor() {
        let con = createDbConnection();
        this.entityManager = getManager();
    }

    async create(user: User) {
        this.entityManager.create(User, user);
        await this.entityManager.save(user);
    }
}
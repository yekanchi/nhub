import {createDbConnection} from "./Service/DataService";
import {createConnection, EntityManager, getManager} from "typeorm";
import {User} from "./domain/Domain";

export async function RunService() {

    let someUser = new User({userName: "m.talebi", name: "morteza", family: "talebi"});
    var connection = await createDbConnection();
    var manager = getManager();
    await manager.create(User, someUser);

    // let repo = new UserRepository();
    // await repo.create(someUser);
}


export class UserRepository {
    constructor() {

    }

    async create(user: User) {
        let con = createDbConnection();
        let entityManager = getManager()
        entityManager.create(User, user);
        await entityManager.save(user);
    }
}
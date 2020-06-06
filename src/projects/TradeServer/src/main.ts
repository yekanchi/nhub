import { createDbConnection } from "./Service/DataService";
import { getManager } from "typeorm";
import { User } from "./domain/Domain";

export async function RunService() {
    var con = await createDbConnection();
    console.log(con);


    var someuser = new User();
    someuser.name = "some user";
    const entityManager = getManager(); // you can also get it via getConnection().manager
    const user = await entityManager.create(User, someuser)
    entityManager.save(someuser);
}
import { getManager, Connection, Repository } from "typeorm";
import { User } from "./domain/Domain";
import { getConnection } from "./getConnection";

export async function RunService() {

    console.log(25 / 0);


    var userRepository = new UserRepository(await getConnection());

    console.log(await userRepository.getAll());
    // while (true) {
    //     let someUser = new User({ userName: "m.talebi", name: "morteza", family: "talebi" });
    //     await userRepository.create(someUser);
    // }

}


export class UserRepository {
    repository: Repository<User>
    constructor(connection: Connection) {
        this.repository = connection.getRepository(User);
    }
    async create(user: User): Promise<User> {
        return this.repository.save(user);;
    }

    async getAll(): Promise<User[]> {
        return await this.repository.find();
    }
}



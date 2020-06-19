import {Connection, Repository} from "typeorm";
import {Letter, User} from "./domain/Domain";
import {getConnection} from "./getConnection";
import {UserRepository} from "./dal/DataAccess";



export async function RunService() {
    let userRepository = new UserRepository(await getConnection());
    console.log(await userRepository.getAll());
    // while (true) {
    //     let someUser = new User({ userName: "m.talebi", name: "morteza", family: "talebi" });
    //     await userRepository.create(someUser);
    // }

}






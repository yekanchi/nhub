import {Letter, User} from "../domain/Domain";
import {EntityRepository, Repository} from "typeorm";

@EntityRepository()
export class UserRepository extends Repository<User> {

    get(id: number) {
        return this.createQueryBuilder("user")
            .where("user.id = :id", { id })
            .getOne();
    }
}

export class DumpDataRepository extends Repository<Letter> {

    get(id: number) {
        return this.createQueryBuilder("user")
            .where("user.id = :id", { id })
            .getOne();
    }
}





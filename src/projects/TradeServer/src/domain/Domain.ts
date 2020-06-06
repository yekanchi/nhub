import {Entity, PrimaryGeneratedColumn, Column} from "typeorm";

@Entity({name: "main.users"})
export class User {

    @PrimaryGeneratedColumn({name: "user_id", type: "integer"})
    id: number;

    // @Column({name: "user_uid"})
    // uid: string;

    @Column({type: "text"})
    name: string;

    // @Column()
    // family: string;

    // @Column()
    // email: string;
}





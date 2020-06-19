import {Letter} from "../domain/Domain";

export interface IDumpRepository {

    insert(letter: Letter): Promise<Letter>;

    getAll(): Promise<Letter[]>;
}
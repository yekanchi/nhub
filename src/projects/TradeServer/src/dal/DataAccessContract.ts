
export interface IDumpRepository {

    create(letter: Letter): Promise<Letter>;

    getAll(): Promise<Letter[]>;
}
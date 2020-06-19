export interface IDumpRepository {
    create(letter: Letter): Promise<Letter>;
    getAll(): Promise<Letter[]>;
}
import { Letter } from "../domain/Domain";
export interface IDumpRepository {
    create(letter: Letter): Promise<Letter>;
    getAll(): Promise<Letter[]>;
}
//# sourceMappingURL=DataAccessContract.d.ts.map
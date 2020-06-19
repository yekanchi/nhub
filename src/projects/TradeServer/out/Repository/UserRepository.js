"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.DumpDataRepository = exports.UserRepository = void 0;
const typeorm_1 = require("typeorm");
let UserRepository = class UserRepository extends typeorm_1.Repository {
    get(id) {
        return this.createQueryBuilder("user")
            .where("user.id = :id", { id })
            .getOne();
    }
};
UserRepository = __decorate([
    typeorm_1.EntityRepository()
], UserRepository);
exports.UserRepository = UserRepository;
class DumpDataRepository extends typeorm_1.Repository {
    get(id) {
        return this.createQueryBuilder("user")
            .where("user.id = :id", { id })
            .getOne();
    }
}
exports.DumpDataRepository = DumpDataRepository;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiVXNlclJlcG9zaXRvcnkuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi9zcmMvUmVwb3NpdG9yeS9Vc2VyUmVwb3NpdG9yeS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7QUFDQSxxQ0FBcUQ7QUFHckQsSUFBYSxjQUFjLEdBQTNCLE1BQWEsY0FBZSxTQUFRLG9CQUFnQjtJQUVoRCxHQUFHLENBQUMsRUFBVTtRQUNWLE9BQU8sSUFBSSxDQUFDLGtCQUFrQixDQUFDLE1BQU0sQ0FBQzthQUNqQyxLQUFLLENBQUMsZUFBZSxFQUFFLEVBQUUsRUFBRSxFQUFFLENBQUM7YUFDOUIsTUFBTSxFQUFFLENBQUM7SUFDbEIsQ0FBQztDQUNKLENBQUE7QUFQWSxjQUFjO0lBRDFCLDBCQUFnQixFQUFFO0dBQ04sY0FBYyxDQU8xQjtBQVBZLHdDQUFjO0FBUzNCLE1BQWEsa0JBQW1CLFNBQVEsb0JBQWtCO0lBRXRELEdBQUcsQ0FBQyxFQUFVO1FBQ1YsT0FBTyxJQUFJLENBQUMsa0JBQWtCLENBQUMsTUFBTSxDQUFDO2FBQ2pDLEtBQUssQ0FBQyxlQUFlLEVBQUUsRUFBRSxFQUFFLEVBQUUsQ0FBQzthQUM5QixNQUFNLEVBQUUsQ0FBQztJQUNsQixDQUFDO0NBQ0o7QUFQRCxnREFPQyJ9
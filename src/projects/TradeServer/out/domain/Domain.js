"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.Letter = exports.User = exports.CodalPage = void 0;
const typeorm_1 = require("typeorm");
class CodalPage {
}
exports.CodalPage = CodalPage;
let User = class User {
    //this is for es6 initialization
    constructor(intial) {
        Object.assign(this, intial);
    }
};
__decorate([
    typeorm_1.PrimaryGeneratedColumn({ name: "user_id", type: "integer" }),
    __metadata("design:type", Number)
], User.prototype, "id", void 0);
__decorate([
    typeorm_1.Column({ name: "user_name", type: "text" }),
    __metadata("design:type", String)
], User.prototype, "userName", void 0);
__decorate([
    typeorm_1.Column({ name: "name", type: "text" }),
    __metadata("design:type", String)
], User.prototype, "name", void 0);
__decorate([
    typeorm_1.Column({ name: "family", type: "text" }),
    __metadata("design:type", String)
], User.prototype, "family", void 0);
__decorate([
    typeorm_1.Column({ name: "email", type: "text" }),
    __metadata("design:type", String)
], User.prototype, "email", void 0);
__decorate([
    typeorm_1.Column({ name: "password_hash", type: "text" }),
    __metadata("design:type", String)
], User.prototype, "password", void 0);
User = __decorate([
    typeorm_1.Entity({ name: "main.users" }),
    __metadata("design:paramtypes", [Object])
], User);
exports.User = User;
let Letter = class Letter {
};
__decorate([
    typeorm_1.PrimaryGeneratedColumn({ name: "user_id", type: "integer" }),
    __metadata("design:type", Number)
], Letter.prototype, "id", void 0);
__decorate([
    typeorm_1.Column({ type: "text" }),
    __metadata("design:type", String)
], Letter.prototype, "attachmentUrl", void 0);
__decorate([
    typeorm_1.Column({ type: "integer" }),
    __metadata("design:type", Number)
], Letter.prototype, "code", void 0);
__decorate([
    typeorm_1.Column({ type: "text" }),
    __metadata("design:type", String)
], Letter.prototype, "companyName", void 0);
__decorate([
    typeorm_1.Column({ type: "integer" }),
    __metadata("design:type", Number)
], Letter.prototype, "publishTime", void 0);
__decorate([
    typeorm_1.Column({ type: "integer" }),
    __metadata("design:type", Number)
], Letter.prototype, "sendTime", void 0);
__decorate([
    typeorm_1.Column({ type: "string" }),
    __metadata("design:type", String)
], Letter.prototype, "symbol", void 0);
__decorate([
    typeorm_1.Column({ type: "integer" }),
    __metadata("design:type", Number)
], Letter.prototype, "tracingNumber", void 0);
__decorate([
    typeorm_1.Column({ type: "text" }),
    __metadata("design:type", String)
], Letter.prototype, "url", void 0);
Letter = __decorate([
    typeorm_1.Entity({ name: "main.letters" })
], Letter);
exports.Letter = Letter;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRG9tYWluLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vc3JjL2RvbWFpbi9Eb21haW4udHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7O0FBQUEscUNBQStEO0FBSS9ELE1BQWEsU0FBUztDQUlyQjtBQUpELDhCQUlDO0FBSUQsSUFBYSxJQUFJLEdBQWpCLE1BQWEsSUFBSTtJQW1CYixnQ0FBZ0M7SUFDaEMsWUFBbUIsTUFBcUI7UUFDcEMsTUFBTSxDQUFDLE1BQU0sQ0FBQyxJQUFJLEVBQUUsTUFBTSxDQUFDLENBQUM7SUFDaEMsQ0FBQztDQUNKLENBQUE7QUFyQkc7SUFEQyxnQ0FBc0IsQ0FBQyxFQUFDLElBQUksRUFBRSxTQUFTLEVBQUUsSUFBSSxFQUFFLFNBQVMsRUFBQyxDQUFDOztnQ0FDaEQ7QUFHWDtJQURDLGdCQUFNLENBQUMsRUFBQyxJQUFJLEVBQUUsV0FBVyxFQUFFLElBQUksRUFBRSxNQUFNLEVBQUMsQ0FBQzs7c0NBQ3pCO0FBR2pCO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxNQUFNLEVBQUUsSUFBSSxFQUFFLE1BQU0sRUFBQyxDQUFDOztrQ0FDeEI7QUFHYjtJQURDLGdCQUFNLENBQUMsRUFBQyxJQUFJLEVBQUUsUUFBUSxFQUFFLElBQUksRUFBRSxNQUFNLEVBQUMsQ0FBQzs7b0NBQ3hCO0FBR2Y7SUFEQyxnQkFBTSxDQUFDLEVBQUMsSUFBSSxFQUFFLE9BQU8sRUFBRSxJQUFJLEVBQUUsTUFBTSxFQUFDLENBQUM7O21DQUN4QjtBQUdkO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxlQUFlLEVBQUUsSUFBSSxFQUFFLE1BQU0sRUFBQyxDQUFDOztzQ0FDN0I7QUFqQlIsSUFBSTtJQURoQixnQkFBTSxDQUFDLEVBQUMsSUFBSSxFQUFFLFlBQVksRUFBQyxDQUFDOztHQUNoQixJQUFJLENBdUJoQjtBQXZCWSxvQkFBSTtBQTJCakIsSUFBYSxNQUFNLEdBQW5CLE1BQWEsTUFBTTtDQTJCbEIsQ0FBQTtBQXpCRztJQURDLGdDQUFzQixDQUFDLEVBQUMsSUFBSSxFQUFFLFNBQVMsRUFBRSxJQUFJLEVBQUUsU0FBUyxFQUFDLENBQUM7O2tDQUNoRDtBQUdYO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxNQUFNLEVBQUMsQ0FBQzs7NkNBQ0Q7QUFHdEI7SUFEQyxnQkFBTSxDQUFDLEVBQUMsSUFBSSxFQUFFLFNBQVMsRUFBQyxDQUFDOztvQ0FDYjtBQUdiO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxNQUFNLEVBQUMsQ0FBQzs7MkNBQ0g7QUFHcEI7SUFEQyxnQkFBTSxDQUFDLEVBQUMsSUFBSSxFQUFFLFNBQVMsRUFBQyxDQUFDOzsyQ0FDTjtBQUdwQjtJQURDLGdCQUFNLENBQUMsRUFBQyxJQUFJLEVBQUUsU0FBUyxFQUFDLENBQUM7O3dDQUNUO0FBR2pCO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxRQUFRLEVBQUMsQ0FBQzs7c0NBQ1Y7QUFHZjtJQURDLGdCQUFNLENBQUMsRUFBQyxJQUFJLEVBQUUsU0FBUyxFQUFDLENBQUM7OzZDQUNKO0FBR3RCO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxNQUFNLEVBQUMsQ0FBQzs7bUNBQ1g7QUExQkgsTUFBTTtJQURsQixnQkFBTSxDQUFDLEVBQUMsSUFBSSxFQUFFLGNBQWMsRUFBQyxDQUFDO0dBQ2xCLE1BQU0sQ0EyQmxCO0FBM0JZLHdCQUFNIn0=
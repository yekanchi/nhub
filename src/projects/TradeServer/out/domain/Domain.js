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
exports.User = void 0;
const typeorm_1 = require("typeorm");
let User = class User {
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
], User.prototype, "passsowrd", void 0);
User = __decorate([
    typeorm_1.Entity({ name: "main.users" }),
    __metadata("design:paramtypes", [Object])
], User);
exports.User = User;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRG9tYWluLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vc3JjL2RvbWFpbi9Eb21haW4udHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7Ozs7Ozs7O0FBQUEscUNBQStEO0FBRy9ELElBQWEsSUFBSSxHQUFqQixNQUFhLElBQUk7SUFvQmIsWUFBbUIsTUFBcUI7UUFDcEMsTUFBTSxDQUFDLE1BQU0sQ0FBQyxJQUFJLEVBQUUsTUFBTSxDQUFDLENBQUM7SUFDaEMsQ0FBQztDQUNKLENBQUE7QUFyQkc7SUFEQyxnQ0FBc0IsQ0FBQyxFQUFDLElBQUksRUFBRSxTQUFTLEVBQUUsSUFBSSxFQUFFLFNBQVMsRUFBQyxDQUFDOztnQ0FDaEQ7QUFHWDtJQURDLGdCQUFNLENBQUMsRUFBQyxJQUFJLEVBQUUsV0FBVyxFQUFFLElBQUksRUFBRSxNQUFNLEVBQUMsQ0FBQzs7c0NBQ3pCO0FBR2pCO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxNQUFNLEVBQUUsSUFBSSxFQUFFLE1BQU0sRUFBQyxDQUFDOztrQ0FDeEI7QUFHYjtJQURDLGdCQUFNLENBQUMsRUFBQyxJQUFJLEVBQUUsUUFBUSxFQUFFLElBQUksRUFBRSxNQUFNLEVBQUMsQ0FBQzs7b0NBQ3hCO0FBR2Y7SUFEQyxnQkFBTSxDQUFDLEVBQUMsSUFBSSxFQUFFLE9BQU8sRUFBRSxJQUFJLEVBQUUsTUFBTSxFQUFDLENBQUM7O21DQUN4QjtBQUdkO0lBREMsZ0JBQU0sQ0FBQyxFQUFDLElBQUksRUFBRSxlQUFlLEVBQUUsSUFBSSxFQUFFLE1BQU0sRUFBQyxDQUFDOzt1Q0FDNUI7QUFqQlQsSUFBSTtJQURoQixnQkFBTSxDQUFDLEVBQUMsSUFBSSxFQUFFLFlBQVksRUFBQyxDQUFDOztHQUNoQixJQUFJLENBdUJoQjtBQXZCWSxvQkFBSSJ9
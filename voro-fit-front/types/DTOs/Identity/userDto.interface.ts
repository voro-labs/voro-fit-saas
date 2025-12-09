import { UserRoleDto } from "./userRoleDto.interface";

export interface UserDto {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  avatarUrl?: string;
  birthDate?: Date;
  phoneNumber?: string;
  isActive: boolean;
  userRoles?: UserRoleDto[];
}
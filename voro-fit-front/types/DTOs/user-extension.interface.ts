import { UserDto } from "./Identity/user.interface"
import { InstanceDto } from "./instance.interface"
import { StudentDto } from "./student.interface"

export interface UserExtensionDto {
  userId?: string
  user?: UserDto
  student?: StudentDto
  instances?: InstanceDto[]
}

import { UserDto } from "./Identity/userDto.interface"
import { InstanceDto } from "./instanceDto.interface"
import { StudentDto } from "./student.interface"

export interface UserExtensionDto {
  userId: string
  user: UserDto
  student?: StudentDto
  instances: InstanceDto[]
}

export interface UserView {
  id: string
  name: string
  surname: string
  age?: number
  description?: string
  avatar?: string
  status?: string
  isSubbed: boolean
  subscribers: number
  followers: number
}

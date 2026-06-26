export interface RunEvent {
  date: string
  location: string
  status: string
  notes: string | null
}

export interface Bar {
  name: string
  address: string
  city: string
  state: string
  zip: string
  latitude: number
  longitude: number
}

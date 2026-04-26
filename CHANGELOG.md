# Changelog

## [1.3.0]
### BREAKING CHANGES
- Materials have been moved to samples that can be imported into the project through the package manager.

### Fixes
-Fixed Cheat Sheet error for UI Binding

## [1.2.2]
### Fixes
- Button Attribute now works correctly with method overloads.

## [1.2.1]
### Fixes
- Made Checklist Items that wrap lines not break the layout.

## [1.2.0]
### BREAKING CHANGES
- Made Button Attribute work without UnityEvents. Just give it the method name now.
- Renamed the old Button Attribute to EventButton.

### Fixes
- Made Checklist Item handle centered for multi-line items.

## [1.1.0]
### Editor Scripts
- Added Units Attribute

### Math
- Added Degree & Radian structs
- Added ProjectOntoPlane and LawOfCosines functions

### DOTS
- Added Gizmo Invoker for ECS Systems
- Added dangle & rangle Unity Mathematics structs
- Added angle, plane projection, mod, and law of cosines math functions

### Fixes
- Fixed build failing for Markdown documents

## [1.0.0] !!!
### Shaders
- Added Skybox Shader
- Added Prototype Shader
- Added Normals Test Shader
- Added Grid Subgraph
- Added Grid Triplanar Subgraph
- Added Random Range 3D Subgraph

### Materials
- Added Skybox Materials
- Added Multi-Colored Prototype Materials
- Added UV Test Material
- Added Normals Test Material

### Models
- Added 16 or so Primitives
- Added Primitive Prefabs to Create > 3D Menu

### Scripts
- Added NoodleTrigger component to visualize and hook up triggers
- Added animation coroutines to NoodleUtils

### Scriptable Objects
- Finished Checklist
- Finished Markdown

### Textures
- Added UV Test Texture
- Added Template Tilesheet (and 5 color variants)

### 2D
- Added Prototype Rule Tiles
- Added Prototype Sprite Palette

### Assets
- Added Roxy's Editor Layouts
- Added Starter Scene Templates

## [0.5.0]

### Cheat Sheet
- Added Animation Section
- Added ProBuilder Section
- Added Profiler Section
- Added Making Builds Section
- Added Shader Graph Section
- Added VFX Graph Section
- Added Rendering Section
- Added Rule Tiles to the 2D Section

## [0.4.0]

### Cheat Sheet
- Added Math Section
- Added 2D Section
- Added Physics Section
- Added Audio Section
- Added UI Section
- Added Managing Data Section
- Added Editor Scripting Section
- Added Multiplayer Section
- Re-Ordered Sections
- Added more headers for future sections

### Editor Scripts
- Added Divider Field Attribute
- Added Button Field Attribute

### Scriptable Objects
- Added Checklist (Assets > Create > Noodle Kit > Checklist)

### Utils
- Added NoodleUtils Class
- Added FormatCamelCase() function to utils

## [0.3.0]

### Cheat Sheet
- Added Input section
- Added Math section
- Added 2D section

## [0.2.0]

### Cheat Sheet
- Added GameObject section
- Added Monobehaviour section
- Added Transform section

## [0.1.0] - Initial Version

- Created Package
- Added CheatSheet.md
